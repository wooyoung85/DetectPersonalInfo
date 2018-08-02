using ClosedXML.Excel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace DetectPesonalInfo
{
    public partial class Detect : Form
    {
        #region [Member]
        private string CONNECT_STRING;
        SqlConnection Conn;
        SqlCommand Cmd;
        SqlDataReader Rdr;

        DataTable Dt_Table;
        DataTable Dt_Detect_Result;
        string um_pattern1 = "[0-9]{1,3}\\.[0-9]{1,}";
        string um_pattern2 = "[0-9]{6}-?[0-9]{8,}";
        string m_pattern = @"(?:[0-9]{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[1,2][0-9]|3[0,1]))-?[1-6][0-9]{6}";

        //String date_pattern = @"(?:(?:(?:(?:(?:[13579][26]|[2468][048])00)|(?:[0-9]{2}(?:(?:[13579][26])|(?:[2468][048]|0[48]))))(?:(?:(?:09|04|06|11)(?:0[1-9]|1[0-9]|2[0-9]|30))|(?:(?:01|03|05|07|08|10|12)(?:0[1-9]|1[0-9]|2[0-9]|3[01]))|(?:02(?:0[1-9]|1[0-9]|2[0-9]))))|(?:[0-9]{4}(?:(?:(?:09|04|06|11)(?:0[1-9]|1[0-9]|2[0-9]|30))|(?:(?:01|03|05|07|08|10|12)(?:0[1-9]|1[0-9]|2[0-9]|3[01]))|(?:02(?:[01][0-9]|2[0-8])))))(?:0[0-9]|1[0-9]|2[0-3])(?:[0-5][0-9]){2}";       

        //String[] pattern_list = {@"(?:[0-9]{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[1,2][0-9]|3[0,1]))[1-6][0-9]{6}",
        //                          "(?:[0-9]{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[1,2][0-9]|3[0,1]))-[1-6][0-9]{6}"};
             
        const Int32 SeperateRowCnt = 200000;
        const int ThreadCnt = 4;

        private string FOLDER_NAME = @"C:\DetectPesonalInfo\" + DateTime.Now.ToString("yyyyMMdd") + "\\";
        private string TABLE_NAME;        
        Dictionary<string, int> DicDetectedTable = new Dictionary<string, int>();
        #endregion

        public Detect()
        {
            InitializeComponent();
            InitializeForm();
        }
        #region [Method]

        #region [폼 초기화]
        /// <summary>
        /// 폼 초기화
        /// </summary>
        private void InitializeForm()
        {
            this.Text = "Detect Pesonal Info";

            gb_Info.Text = "  안내사항\n";
            gb_Info.Text += "1. MS-SQL DB만 접근가능\n";
            gb_Info.Text += "2. DB 접속 후 개인정보(주민등록번호) 검출할 테이블 리스트 선택";

            btnConnect.Visible = true;
            btnConnectChange.Visible = false;
            txtServerIP.Enabled = true;
            txtDbmsName.Enabled = true;
            txtID.Enabled = true;
            txtPassWord.Enabled = true;
            btnDetect.Visible = false;
            gv_TableList.DataSource = null;
            gv_TagetTableList.DataSource = null;
            DicDetectedTable.Clear();
            CONNECT_STRING = string.Empty;
            progressBar.Value = 0;
            lblStartTime.Text = "...";
            lblEndTime.Text = "...";
            lblRunningTime.Text = "...";
        }
        #endregion

        #region [개인정보 검출 작업]
        /// <summary>
        /// 개인정보 검출 작업
        /// </summary>
        /// <param name="tbl">테이블명</param>
        private void Start_DetectPesonalInfo(string tbl)
        {
            Conn = new SqlConnection(CONNECT_STRING);
            Dt_Detect_Result = new DataTable();
            TABLE_NAME = tbl;

            try
            {
                Conn.Open();
                Cmd = new SqlCommand("SELECT COUNT(1) FROM " + TABLE_NAME, Conn);
                Int32 dtCnt = Convert.ToInt32(Cmd.ExecuteScalar());

                if (dtCnt > SeperateRowCnt)
                {
                    int count = dtCnt / SeperateRowCnt;
                    for (int i = 0; i < count + 1; i++)
                    {
                        Cmd = new SqlCommand("SELECT * FROM " + TABLE_NAME + " ORDER BY 1  OFFSET " + i * SeperateRowCnt + " ROW FETCH NEXT " + SeperateRowCnt + " ROWS ONLY", Conn);
                        Dt_Table = new DataTable();
                        DetectJuminNo();
                    }
                }
                else
                {
                    Cmd = new SqlCommand("SELECT * FROM " + TABLE_NAME, Conn);
                    Dt_Table = new DataTable();
                    DetectJuminNo();
                }

                int result_cnt = Dt_Detect_Result.Rows.Count;
                DicDetectedTable.Add(TABLE_NAME, result_cnt);

                if (result_cnt > 0)
                {
                    CreateResultExcelFile();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("개인정보 검출 중 에러 발생\n\n Error Message : " + ex);
            }
        }
        #endregion

        #region [주민번호 찾기]
        /// <summary>
        /// 주민번호 찾기
        /// </summary>
        private void DetectJuminNo()
        {
            Rdr = Cmd.ExecuteReader();
            Dt_Table.Load(Rdr);
            if (Dt_Detect_Result.Rows.Count < 1)
                Dt_Detect_Result = Dt_Table.Clone();

            progressBar.Step = 10;
            progressBar.PerformStep();
            progressBar.Maximum = Dt_Table.Rows.Count;
            progressBar.Step = 1;

            int dt_cnt = Dt_Table.Rows.Count;
            int thread_dt_cnt = SeperateRowCnt / ThreadCnt;

            if (dt_cnt > thread_dt_cnt)
            {
                // Thread 갯수 지정
                int count = (dt_cnt % thread_dt_cnt == 0) ? (dt_cnt / thread_dt_cnt) : (dt_cnt / thread_dt_cnt + 1);
                Thread[] threads = new Thread[count];

                // Thread 갯수 만큼 Thread 생성
                for (int i = 0; i < threads.Length; i++)
                {
                    // 인자 값이 있으므로 ParameterizedThreadStart 사용
                    threads[i] = new Thread(new ParameterizedThreadStart(WorkingThread));
                }

                int step = 0; // 인자 값중에서 쓰레드 갯수를 세기 위해서 사용
                // threads 안에서 thread 갯수만큼 Thread 호출
                foreach (Thread thread in threads)
                {
                    DataTable dt_thread = new DataTable();
                    dt_thread = Partition(Dt_Table, (thread_dt_cnt * step) + 1, thread_dt_cnt * (step + 1)).CopyToDataTable();
                    thread.Start(new object[] { dt_thread, step++ });
                }

                // Thread 갯수만큼 작업이 끝날때까지 대기 
                foreach (Thread thread in threads)
                {
                    thread.Join();
                    progressBar.PerformStep();
                }
            }
            else
            {
                foreach (DataRow row in Dt_Table.Rows) // Loop over the rows.
                {
                    foreach (var item in row.ItemArray) // Loop over the items.
                    {
                        if (item.GetType() != typeof(int) && item.GetType() != typeof(DateTime))
                        {
                            //Match um1 = Regex.Match(item.ToString(), um_pattern1);
                            //Match um2 = Regex.Match(item.ToString(), um_pattern2);
                            ////Match m = Regex.Match(item.ToString(), String.Format("({0})", String.Join("|", pattern_list)));
                            //Match m = Regex.Match(item.ToString(), m_pattern);

                            if (CheckValue(item.ToString()))
                            {
                                Dt_Detect_Result.Rows.Add(row.ItemArray);
                            }
                        }
                    }
                    progressBar.PerformStep();
                }
            }
            Rdr = null;
        }
        #endregion

        #region [Thread 작업용 DataTable 분할]
        /// <summary>
        /// DataTable 분할
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="index">시작index</param>
        /// <param name="endIndex">끝index</param>
        private static IEnumerable<DataRow> Partition(DataTable dt, int index, int endIndex)
        {
            for (var i = index; i < endIndex && i < dt.Rows.Count; i++)
            {
                yield return dt.Rows[i];
            }
        }
        #endregion

        #region [Thread 수행 작업]
        /// <summary>
        /// Thread 수행 작업
        /// </summary>
        /// <param name="info">작업대상</param>
        private void WorkingThread(object info)
        {
            DataTable dt = (DataTable)((object[])info)[0];

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows) // Loop over the rows.
                {
                    foreach (var item in row.ItemArray) // Loop over the items.
                    {
                        if (item.GetType() != typeof(int) && item.GetType() != typeof(DateTime))
                        {
                            //Match um1 = Regex.Match(item.ToString(), um_pattern1, RegexOptions.Compiled);
                            //Match um2 = Regex.Match(item.ToString(), um_pattern2, RegexOptions.Compiled);
                            //Match m = Regex.Match(item.ToString(), m_pattern, RegexOptions.Compiled);
                            //Match m = Regex.Match(item.ToString(), String.Format("({0})", String.Join("|", pattern_list)));

                            if (CheckValue(item.ToString()))
                            {
                                lock (Dt_Detect_Result.Rows.SyncRoot)
                                {
                                    Dt_Detect_Result.Rows.Add(row.ItemArray);
                                }
                            }
                        }
                    }
                }
            }
            Thread.Sleep(100);
        }
        #endregion

        private bool CheckValue(string val)
        {
            Match um1 = Regex.Match(val, um_pattern1);
            Match um2 = Regex.Match(val, um_pattern2);            

            if (!(um1.Success || um2.Success))
            {
                Match m = Regex.Match(val, m_pattern);

                if (m.Success && CheckJuminNo(Convert.ToString(m.Value)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
                
        }

        #region [주민번호 유효성 체크]
        /// <summary>
        /// 주민번호 유효성 체크
        /// </summary>
        /// <param name="jumin"></param>
        /// <returns></returns>
        private bool CheckJuminNo(string jumin)
        {
            jumin = jumin.Replace(" ", "").Replace("-", "");

            if (jumin.Length != 13)
            {
                return false;
            }

            int sum = 0;
            for (int i = 0; i < jumin.Length - 1; i++)
            {
                char c = jumin[i];

                if (!char.IsNumber(c))
                {
                    return false;
                }
                else
                {
                    if (i < jumin.Length)
                    {
                        sum += int.Parse(c.ToString()) * ((i % 8) + 2);
                    }
                }
            }

            if (!((((11 - (sum % 11)) % 10).ToString()) == ((jumin[jumin.Length - 1]).ToString())))
            {
                return false;
            }
            return true;
        } 
        #endregion

        #region [엑셀 결과 파일 생성]
        /// <summary>
        /// 엑셀 결과 파일 생성
        /// </summary>
        private void CreateResultExcelFile()
        {
            
            string SaveAsName = FOLDER_NAME + TABLE_NAME + ".xlsx";

            if (!Directory.Exists(FOLDER_NAME))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(FOLDER_NAME));
            }
            if (File.Exists(SaveAsName))
            {
                File.Delete(SaveAsName);
            }

            foreach (DataRow row in Dt_Detect_Result.Rows)
            {
                for (int i = 0; i < Dt_Detect_Result.Columns.Count; i++)
                {
                    if (Dt_Detect_Result.Columns[i].DataType == typeof(string))
                        row[i] = ((object)row[i] == DBNull.Value) ? string.Empty : ReplaceHexadecimalSymbols((string)row[i].ToString());
                }
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                using (var ws = wb.AddWorksheet("Result"))
                {
                    ws.Cell(1, 1).InsertTable(Dt_Detect_Result);

                    int colcnt = ws.RangeUsed().ColumnCount();
                    int rowcnt = ws.RangeUsed().RowCount();

                    for (int i = 1; i < rowcnt + 1; i++)
                    {
                        for (int j = 1; j < colcnt + 1; j++)
                        {
                            string t = ws.Cell(i, j).Value.ToString();
                            Match um1 = Regex.Match(ws.Cell(i, j).Value.ToString(), um_pattern1);
                            Match um2 = Regex.Match(ws.Cell(i, j).Value.ToString(), um_pattern2);
                            Match m = Regex.Match(ws.Cell(i, j).Value.ToString(), m_pattern);

                            if (!um1.Success && !um2.Success && m.Success)
                                ws.Cell(i, j).Style.Fill.SetBackgroundColor(XLColor.BabyBlue);
                        }
                    }
                }
                wb.SaveAs(SaveAsName);
            }
        }

        /// <summary>
        /// 16진수 변환 관련 오류를 위한 Replace
        /// </summary>
        static string ReplaceHexadecimalSymbols(string txt)
        {
            string r = "[\x00-\x08\x0B\x0C\x0E-\x1F\x26]";
            return Regex.Replace(txt, r, "", RegexOptions.Compiled);
        } 
        #endregion

        #region [개인정보검출결과 바인딩]
        /// <summary>
        /// 개인정보검출결과 바인딩
        /// </summary>
        /// <param name="arr">대상테이블명 ArrayList</param>
        private void Bind_TagetTableList(ArrayList arr)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Count", typeof(int));
            dt.Columns.Add("TABLE_NAME", typeof(string));

            foreach (string tbl in arr)
            {
                DataRow row = dt.NewRow();
                row["TABLE_NAME"] = tbl;
                row["COUNT"] = DicDetectedTable[tbl];

                dt.Rows.Add(row);
            }

            gv_TagetTableList.DataSource = dt;
            gv_TagetTableList.Columns["COUNT"].DisplayIndex = 0;

            foreach (DataGridViewRow row in gv_TagetTableList.Rows)
            {
                if (Convert.ToInt32(row.Cells["COUNT"].Value) > 0)
                {
                    row.DefaultCellStyle.BackColor = Color.Yellow;
                }
            }
        }
        #endregion

        #endregion

        #region [Click Event]

        #region [DB연결 후 테이블 List 조회]
        /// <summary>
        /// DB연결 후 테이블 List 조회
        /// </summary>
        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtServerIP.Text))
            {
                MessageBox.Show("서버 IP를 입력하시기 바랍니다. \n ex) xxx.xxx.xxx.xxx");
                txtServerIP.Focus();
                return;
            }
            else if (String.IsNullOrEmpty(txtDbmsName.Text))
            {
                MessageBox.Show("DBMS명을 입력하시기 바랍니다.");
                txtDbmsName.Focus();
                return;
            }
            else if (String.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show("ID를 입력하시기 바랍니다.");
                txtID.Focus();
                return;
            }
            else if (String.IsNullOrEmpty(txtPassWord.Text))
            {
                MessageBox.Show("PassWord를 입력하시기 바랍니다.");
                txtPassWord.Focus();
                return;
            }

            CONNECT_STRING = string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3}"
                , txtServerIP.Text
                , txtDbmsName.Text
                , txtID.Text
                , txtPassWord.Text);

            Conn = new SqlConnection(CONNECT_STRING);
            Rdr = null;
            DataTable dtTableList = new DataTable();

            Cmd = new SqlCommand("with tab_count(num, TABLE_SCHEMA, TABLE_NAME) as (select row_number() over (order by table_name), TABLE_SCHEMA, table_name from INFORMATION_SCHEMA.TABLES where table_type = 'BASE TABLE') select TABLE_SCHEMA + '.' + '[' + TABLE_NAME + ']' AS TABLE_NAME from tab_count ORDER BY 1", Conn);

            try
            {
                Conn.Open();
                Rdr = Cmd.ExecuteReader();
                dtTableList.Load(Rdr);

                gv_TableList.DataSource = dtTableList;
                gv_TableList.AllowUserToAddRows = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("DB에 접속할 수 없습니다.\n\n Error Message : " + ex);
            }
            finally
            {
                if (Rdr != null) Rdr.Close();
                if (Conn != null) Conn.Close();
                dtTableList.Dispose();

                btnConnect.Visible = false;
                btnConnectChange.Visible = true;
                txtServerIP.Enabled = false;
                txtDbmsName.Enabled = false;
                txtID.Enabled = false;
                txtPassWord.Enabled = false;
                btnDetect.Visible = true;
            }
        }
        #endregion

        #region [대상 테이블 입력]
        /// <summary>
        /// 대상 테이블 입력
        /// </summary>
        private void btnTargetInsert_Click(object sender, EventArgs e)
        {
            DataTable dt_Target = ((DataTable)gv_TableList.DataSource).Clone();
            foreach (DataGridViewRow row in gv_TableList.SelectedRows)
            {
                dt_Target.ImportRow(((DataTable)gv_TableList.DataSource).Rows[row.Index]);
            }
            dt_Target.AcceptChanges();

            if (dt_Target.Rows.Count < 1)
            {
                MessageBox.Show("테이블을 선택해 주시기 바랍니다.");
                gv_TableList.Focus();
                return;
            }
            else
            {
                gv_TagetTableList.DataSource = dt_Target;
                gv_TagetTableList.AllowUserToAddRows = false;
                gv_TableList.ClearSelection();
                gv_TagetTableList.ClearSelection();
            }
        }
        #endregion

        #region [개인정보 검출]
        /// <summary>
        /// 개인정보 검출
        /// </summary>
        private void btnDetect_Click(object sender, EventArgs e)
        {
            if (gv_TagetTableList.Rows.Count <= 0)
            {
                MessageBox.Show("테이블 선택 후 개인정보 검출을 진행하시기 바랍니다.");
                return;
            }

            ArrayList arr_Sel_Tbl = new ArrayList();

            for (int i = 0; i < gv_TagetTableList.Rows.Count; i++)
            {
                arr_Sel_Tbl.Add(gv_TagetTableList.Rows[i].Cells["TABLE_NAME"].Value.ToString());
            }

            if (MessageBox.Show(arr_Sel_Tbl.Count + "개 테이블에 대해 개인정보 검출을 시작하시겠습니까?.", "확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                lblStartTime.Text = DateTime.Now.ToString("hh:mm:ss");

                foreach (string tbl in arr_Sel_Tbl)
                {
                    progressBar.Value = 0;

                    if (!DicDetectedTable.ContainsKey(tbl))
                    {
                        Start_DetectPesonalInfo(tbl);
                    }
                }
                lblEndTime.Text = DateTime.Now.ToString("hh:mm:ss");
                TimeSpan time = TimeSpan.FromSeconds((Convert.ToDateTime(lblEndTime.Text) - Convert.ToDateTime(lblStartTime.Text)).TotalSeconds);
                lblRunningTime.Text = time.ToString(@"hh\:mm\:ss");
                if (Rdr != null) Rdr.Close();
                if (Conn != null) Conn.Close();
                Dt_Table.Clear();
                Dt_Detect_Result.Clear();

                Bind_TagetTableList(arr_Sel_Tbl);
            }
        }
        #endregion

        #region [DB접속정보 변경 & 폼 초기화]
        /// <summary>
        /// DB접속정보 변경 & 폼 초기화
        /// </summary>
        private void btnConnectChange_Click(object sender, EventArgs e)
        {
            InitializeForm();
        }
        #endregion

        #region [개인정보 검출 결과 더블 클릭]
        /// <summary>
        /// 개인정보 검출 결과 더블 클릭
        /// </summary>        
        private void gv_TagetTableList_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Convert.ToInt32(gv_TagetTableList.Rows[e.RowIndex].Cells["COUNT"].Value) > 0)
            {
                var excelApp = new Microsoft.Office.Interop.Excel.Application();
                excelApp.Visible = true;
                excelApp.Workbooks.Open(FOLDER_NAME + gv_TagetTableList.Rows[e.RowIndex].Cells["TABLE_NAME"].Value.ToString() + ".xlsx");
            }
            else
                return;
        }
        #endregion

        #endregion
    }
}
