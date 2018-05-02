using MaterialSkin;
using MaterialSkin.Controls;
using Microsoft.WindowsAPICodePack.Taskbar;
using ScanFriendFB.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace ScanFriendFB
{
    public partial class MainFrm : MaterialForm
    {
        #region Biến

        private readonly MaterialSkinManager skinManager;

        private int colorSchemeIndex;
        public string token = string.Empty;

        //
        private HashSet<User> users = new HashSet<User>();

        private HashSet<Reaction> likes = new HashSet<Reaction>();
        private HashSet<Comment> cmts = new HashSet<Comment>();
        private HashSet<Post> post = new HashSet<Post>();
        private HashSet<Reaction> lus = new HashSet<Reaction>();
        private HashSet<Comment> cus = new HashSet<Comment>();
        private HashSet<User> usersui = new HashSet<User>();

        //
        private DataTable dtStatistic = new DataTable();

        private DataTable dtPost = new DataTable();
        private DataTable dtAction = new DataTable();
        private DataTable dtDeath = new DataTable();
        private DataTable dtUser = new DataTable();

        //
        private int countDeathList;

        private int count;
        private int countPost;
        private int countAction;
        private int countUser;

        #endregion Biến

        #region Phương thức

        public MainFrm()
        {
            InitializeComponent();
            skinManager = MaterialSkinManager.Instance;
            skinManager.AddFormToManage(this);
            skinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            SetColor(Settings.Default.skin.GetHashCode());
            InitTable();
            cbbAction.SelectedIndex = 0;
            checkItem.CheckedChanged += new EventHandler(radioButtons_CheckedChanged);
            checkTotal.CheckedChanged += new EventHandler(radioButtons_CheckedChanged);
            checkAll.CheckedChanged += new EventHandler(radioButtons_CheckedChanged);
            ToolTip tooltip = new ToolTip();
            tooltip.AutoPopDelay = 5000;
            tooltip.InitialDelay = 1000;
            tooltip.ReshowDelay = 500;
            tooltip.ShowAlways = true;
            tooltip.SetToolTip(this.btnScan, "1. Nhấn để thống kê danh sách bạn bè!");
            tooltip.SetToolTip(this.btnPrint, "2. Nhấn để in thống kê danh sách bạn bè!");
            tooltip.SetToolTip(this.checkItem, "3.1. Chọn để thêm bạn bè vào danh sách sắp xóa!");
            tooltip.SetToolTip(this.checkAll, "3.2. Chọn để thêm tất cả bạn bè vào danh sách xóa!");
            tooltip.SetToolTip(this.checkTotal, "3.3.1 Chọn mức tổng like và comment muốn thêm bạn bè vào danh sách xóa!");

            tooltip.SetToolTip(this.numInteractive, "3.3.2 Nhập mức tổng like và comment của bạn bè muốn thêm vào danh sách xóa!");
            tooltip.SetToolTip(this.btnCopy, "4. Nhấn để thêm bạn bè vào danh sách xóa!");
            tooltip.SetToolTip(this.btnCleanItem, "5. Nhấn để xóa bạn bè khỏi danh sách xóa!");
            tooltip.SetToolTip(this.btnUnf, "6. Nhấn để hủy kết bạn với bạn bè nằm trong danh sách xóa!");
            tooltip.SetToolTip(this.btnLogOut, "Nhấn để thoát khỏi chương trình!");
            tooltip.SetToolTip(this.dataGr, "Danh sách thống kê lượt tương tác của bạn bè");
            tooltip.SetToolTip(this.dgvDeathList, "Death Node bạn bè muốn hủy kết bạn");
            tooltip.SetToolTip(this.numPost, "3. Nhập số lượng và nhấn Entel để thống kê danh sách bài đăng!");
            tooltip.SetToolTip(this.btnStatist, "1. Nhấn để thống kê danh sách bài đăng!");
            tooltip.SetToolTip(this.btnPriStatist, "2. Nhấn để in thống kê danh sách bài đăng");
            tooltip.SetToolTip(this.dataPost, "Danh sách thống kê bài đăng!");
            tooltip.SetToolTip(this.cbbAction, "4. Chọn hành động muốn xem!");
            tooltip.SetToolTip(this.btnAction, "5. Nhấn để xem hành động!");
            tooltip.SetToolTip(this.btnUserUI, "1. Nhấn để thống kê danh sách người tương tác trên danh sách các bài đăng!");
            tooltip.SetToolTip(this.dataAction, "Danh sách các hành động!");
            tooltip.SetToolTip(this.btnPriStatist, "2. Nhấn để in thống kê danh sách người tương tác trên danh sách các bài đăng!");
            tooltip.SetToolTip(this.dataUserUI, "Danh sách người tương tác trên danh sách các bài đăng!");
            tooltip.SetToolTip(this.btnSkin, "Nhấn để thay đổi màu cho chương trình!");
        }

        private void SetColor(int v)
        {
            if (v > 7) v = 0;

            //These are just example color schemes
            switch (v)
            {
                case 0:
                    skinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
                    break;

                case 1:
                    skinManager.ColorScheme = new ColorScheme(Primary.Indigo500, Primary.Indigo700, Primary.Indigo100, Accent.Pink200, TextShade.WHITE);
                    break;

                case 2:
                    skinManager.ColorScheme = new ColorScheme(Primary.Green600, Primary.Green700, Primary.Green200, Accent.Red100, TextShade.WHITE);
                    break;

                case 3:
                    skinManager.ColorScheme = new ColorScheme(Primary.Red600, Primary.Red700, Primary.Red200, Accent.Green100, TextShade.WHITE);
                    break;

                case 4:
                    skinManager.ColorScheme = new ColorScheme(Primary.Pink600, Primary.Pink700, Primary.Pink200, Accent.Green100, TextShade.WHITE);
                    break;

                case 5:
                    skinManager.ColorScheme = new ColorScheme(Primary.Purple600, Primary.Purple700, Primary.Purple200, Accent.Red100, TextShade.WHITE);
                    break;

                case 6:
                    skinManager.ColorScheme = new ColorScheme(Primary.Yellow600, Primary.Yellow700, Primary.Yellow200, Accent.Blue100, TextShade.WHITE);
                    break;

                case 7:
                    skinManager.ColorScheme = new ColorScheme(Primary.Blue600, Primary.Blue700, Primary.Blue200, Accent.Yellow100, TextShade.WHITE);
                    break;
            }
        }

        public void updateStatus(string str)
        {
            base.Invoke((MethodInvoker)delegate
            {
                try
                {
                    lbTB.Text = str;
                }
                catch
                {
                }
            });
        }

        private bool RemoveInStatistic(string uid)
        {
            int i = 0;
            base.Invoke((MethodInvoker)delegate
            {
                try
                {
                    dgvDeathList.Sort(dgvDeathList.Columns[4], ListSortDirection.Descending);
                    i = 0;
                    foreach (DataGridViewRow item in this.dgvDeathList.Rows)
                    {
                        dgvDeathList.Rows[i].Cells[0].Value = i + 1;
                        i++;
                    }
                }
                catch (Exception ex)
                {
                    FBGraph.Instance.WriteLog(ex.ToString());
                }
            });
            i = dataGr.Rows.Count - 1;
            foreach (DataGridViewRow item in this.dataGr.Rows)
            {
                string uidCell = dataGr.Rows[i].Cells[2].Value.ToString();
                if (uid.Equals(uidCell))
                {
                    dtStatistic.Rows.RemoveAt(i);
                    base.Invoke((MethodInvoker)delegate
                    {
                        dataGr.Rows.RemoveAt(i);
                    });
                    updateStatus("Hoàn thành!");
                    return true;
                }
                i--;
            }
            updateStatus("Lỗi!");
            return false;
        }

        private void InitTable()
        {
            dtStatistic.Columns.Add("fcheck");
            dtStatistic.Columns.Add("fstt");
            dtStatistic.Columns.Add("fid");
            dtStatistic.Columns.Add("fname");
            dtStatistic.Columns.Add("frelationship_status");
            dtStatistic.Columns.Add("fage_range");
            dtStatistic.Columns.Add("flike");
            dtStatistic.Columns.Add("fcmt");
            dtStatistic.Columns.Add("fsum");
            dtDeath.Columns.Add("dlKeep");
            dtDeath.Columns.Add("dlNo");
            dtDeath.Columns.Add("dlUid");
            dtDeath.Columns.Add("dlName");
            dtDeath.Columns.Add("drelationship_status");
            dtDeath.Columns.Add("dage_range");
            dtDeath.Columns.Add("dlSum");
            dtPost.Columns.Add("fstt");
            dtPost.Columns.Add("fid");
            dtPost.Columns.Add("fdate");
            dtPost.Columns.Add("fcontent");
            dtPost.Columns.Add("fstory");
            dtPost.Columns.Add("flink");
            dtPost.Columns.Add("ftag");
            dtPost.Columns.Add("flike");
            dtPost.Columns.Add("fcmt");
            dtPost.Columns.Add("fsum");
            dtAction.Columns.Add("fstt");
            dtAction.Columns.Add("fid");
            dtAction.Columns.Add("fname");
            dtAction.Columns.Add("ftype");
            dtUser.Columns.Add("fstt");
            dtUser.Columns.Add("fid");
            dtUser.Columns.Add("fname");
            dtUser.Columns.Add("flike");
            dtUser.Columns.Add("fcmt");
            dtUser.Columns.Add("fsum");
        }

        private void AddRow(HashSet<User> user)
        {
            foreach (var item in user)
            {
                DataRow row = dtStatistic.NewRow();
                count++;
                row["fcheck"] = "true";
                row["fstt"] = count;
                row["fid"] = item.id;
                row["fname"] = item.name;
                row["frelationship_status"] = item.relationship_status;
                row["fage_range"] = item.age_range.ToShortDateString();
                row["flike"] = item.likes;
                row["fcmt"] = item.cmts;
                row["fsum"] = item.sum;
                dtStatistic.Rows.Add(row);
                base.Invoke((MethodInvoker)delegate
                {
                    try
                    {
                        int dem = dataGr.Rows.Add();
                        dataGr.Rows[dem].Cells[0].Value = row["fcheck"].ToString();
                        dataGr.Rows[dem].Cells[1].Value = int.Parse(row["fstt"].ToString());
                        dataGr.Rows[dem].Cells[2].Value = row["fid"].ToString();
                        dataGr.Rows[dem].Cells[3].Value = row["fname"].ToString();
                        dataGr.Rows[dem].Cells[4].Value = row["frelationship_status"].ToString();
                        dataGr.Rows[dem].Cells[5].Value = DateTime.Parse(row["fage_range"].ToString()).ToShortDateString();
                        dataGr.Rows[dem].Cells[6].Value = row["flike"];
                        dataGr.Rows[dem].Cells[7].Value = row["fcmt"];
                        dataGr.Rows[dem].Cells[8].Value = int.Parse(row["fsum"].ToString());
                        dataGr.FirstDisplayedScrollingRowIndex = dem;
                    }
                    catch (Exception ex)
                    {
                        FBGraph.Instance.WriteLog(ex.ToString());
                    }
                });
            }
        }

        private void CopyRow(User user)
        {
            DataRow row = dtDeath.NewRow();
            countDeathList++;
            row["dlKeep"] = "true";
            row["dlNo"] = countDeathList;
            row["dlUid"] = user.id;
            row["dlName"] = user.name;
            row["drelationship_status"] = user.relationship_status;
            row["dage_range"] = user.age_range.ToShortDateString();
            row["dlSum"] = user.sum;

            dtDeath.Rows.Add(row);
            base.Invoke((MethodInvoker)delegate
            {
                try
                {
                    int dem = dgvDeathList.Rows.Add();
                    dgvDeathList.Rows[dem].Cells[0].Value = row["dlKeep"].ToString();
                    dgvDeathList.Rows[dem].Cells[1].Value = int.Parse(row["dlNo"].ToString());
                    dgvDeathList.Rows[dem].Cells[2].Value = row["dlUid"].ToString();
                    dgvDeathList.Rows[dem].Cells[3].Value = row["dlName"].ToString();
                    dgvDeathList.Rows[dem].Cells[4].Value = row["drelationship_status"].ToString();
                    dgvDeathList.Rows[dem].Cells[5].Value = DateTime.Parse(row["dage_range"].ToString()).ToShortDateString();
                    dgvDeathList.Rows[dem].Cells[6].Value = row["dlSum"];

                    dgvDeathList.FirstDisplayedScrollingRowIndex = dem;
                }
                catch (Exception ex)
                {
                    FBGraph.Instance.WriteLog(ex.ToString());
                }
            });
        }

        private void AddRowPost(HashSet<Post> po, int countpost)
        {
            foreach (var item in po.Take(countpost))
            {
                DataRow row = dtPost.NewRow();
                countPost++;
                row["fstt"] = countPost;
                row["fid"] = item.id;
                row["fdate"] = item.created_time.ToShortDateString();
                row["fcontent"] = item.message;
                row["fstory"] = item.story;
                row["flink"] = item.link;
                string tag = "";
                if (item.with_tag.Count() > 0)
                {
                    foreach (var ite in item.with_tag)
                    {
                        tag += ite.name + ", ";
                    }
                }
                row["ftag"] = tag;
                row["flike"] = item.reactions.Count();
                row["fcmt"] = item.cmts.Count();
                row["fsum"] = (item.reactions.Count() + item.cmts.Count());
                dtPost.Rows.Add(row);
                base.Invoke((MethodInvoker)delegate
                {
                    try
                    {
                        int dem = dataPost.Rows.Add();
                        dataPost.Rows[dem].Cells[0].Value = int.Parse(row["fstt"].ToString());
                        dataPost.Rows[dem].Cells[1].Value = row["fid"];
                        dataPost.Rows[dem].Cells[2].Value = DateTime.Parse(row["fdate"].ToString()).ToShortDateString();
                        dataPost.Rows[dem].Cells[3].Value = row["fcontent"];
                        dataPost.Rows[dem].Cells[4].Value = row["fstory"];
                        dataPost.Rows[dem].Cells[5].Value = row["flink"];
                        dataPost.Rows[dem].Cells[6].Value = row["ftag"];
                        dataPost.Rows[dem].Cells[7].Value = int.Parse(row["flike"].ToString());
                        dataPost.Rows[dem].Cells[8].Value = int.Parse(row["fcmt"].ToString());
                        dataPost.Rows[dem].Cells[9].Value = int.Parse(row["fsum"].ToString());
                        dataPost.FirstDisplayedScrollingRowIndex = dem;
                    }
                    catch (Exception ex)
                    {
                        FBGraph.Instance.WriteLog(ex.ToString());
                    }
                });
            }
        }

        #endregion Phương thức

        #region Sự kiện

        private void btnUnf_Click(object sender, EventArgs e)
        {
            MainFrm mainFrm;
            new Thread((ThreadStart)delegate
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                try
                {
                    base.Invoke((MethodInvoker)delegate
                    {
                        btnUnf.Enabled = false;
                        btnScan.Enabled = false;
                        checkAll.Enabled = false;
                        checkItem.Enabled = false;
                        checkTotal.Enabled = false;
                        btnPrint.Enabled = false;
                        numInteractive.Enabled = false;
                        btnCleanItem.Enabled = false;
                    });
                    mainFrm = this;
                    if (MessageBox.Show($"Bạn có muốn chia tay với {dgvDeathList.Rows.Count} bạn bè này không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        for (int i = dgvDeathList.RowCount - 1; i >= 0; i--)
                        {
                            if ((bool)dgvDeathList.Rows[i].Cells[0].FormattedValue)
                            {
                                base.Invoke((MethodInvoker)delegate
                                {
                                    mainFrm.dgvDeathList.FirstDisplayedScrollingRowIndex = i;
                                });
                                string text = dgvDeathList.Rows[i].Cells[2].Value.ToString();
                                string text2 = dgvDeathList.Rows[i].Cells[3].Value.ToString();

                                if (dgvDeathList.Rows[i].Cells[2].FormattedValue.ToString().Contains("100006658660077"))
                                {
                                    updateStatus($"Không thể chia tay với {text2} ({text}), Chủ phần mềm này!");
                                }
                                else
                                {
                                    updateStatus($"Chia tay với {text2} ({text})");
                                    if (FBGraph.Instance.Unfriend(text))
                                    {
                                        dtDeath.Rows.RemoveAt(i);
                                        base.Invoke((MethodInvoker)delegate
                                        {
                                            mainFrm.dgvDeathList.Rows.RemoveAt(i);
                                        });
                                        RemoveInStatistic(text);
                                    }
                                    TimeSpan ts = stopWatch.Elapsed;
                                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                        ts.Hours, ts.Minutes, ts.Seconds,
                                        ts.Milliseconds / 10);
                                    updateStatus($"Hoàn thành! Tổng thời gian: {elapsedTime}");
                                }
                            }
                            else
                            {
                                updateStatus("Vui lòng chọn người bạn muốn chia tay!");
                            }
                        }
                    }
                    else
                    {
                        updateStatus("Vui lòng chọn người bạn muốn chia tay!");
                    }
                }
                catch
                {
                    updateStatus("Vui lòng chọn người bạn muốn chia tay!");
                }
                base.Invoke((MethodInvoker)delegate
                {
                    btnUnf.Enabled = true;
                    btnScan.Enabled = true;
                    checkAll.Enabled = true;
                    checkItem.Enabled = true;
                    checkTotal.Enabled = true;
                    btnPrint.Enabled = true;
                    numInteractive.Enabled = true;
                    btnCleanItem.Enabled = true;
                });
            }).Start();
        }

        private void radioButtons_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;

            if (checkItem.Checked)
            {
                numInteractive.Enabled = false;
                int i = dataGr.Rows.Count - 1;
                foreach (DataGridViewRow item in this.dataGr.Rows)
                {
                    if ((bool)dataGr.Rows[i].Cells[0].FormattedValue)
                    {
                        dataGr.Rows[i].Cells[0].Value = "false";
                    }
                    i--;
                }
            }
            else if (checkTotal.Checked)
            {
                numInteractive.Enabled = true;
                int i = dataGr.Rows.Count - 1;
                foreach (DataGridViewRow item in this.dataGr.Rows)
                {
                    if (!(bool)dataGr.Rows[i].Cells[0].FormattedValue)
                    {
                        dataGr.Rows[i].Cells[0].Value = "true";
                    }
                    i--;
                }
            }
            else if (checkAll.Checked)
            {
                numInteractive.Enabled = false;
                int i = dataGr.Rows.Count - 1;
                foreach (DataGridViewRow item in this.dataGr.Rows)
                {
                    if (!(bool)dataGr.Rows[i].Cells[0].FormattedValue)
                    {
                        dataGr.Rows[i].Cells[0].Value = "true";
                    }
                    i--;
                }
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            MainFrm mainFrm;
            new Thread((ThreadStart)delegate
            {
                try
                {
                    base.Invoke((MethodInvoker)delegate
                    {
                        dgvDeathList.Rows.Clear();
                        dgvDeathList.DataSource = null;
                        btnUnf.Enabled = false;
                        btnCleanItem.Enabled = false;
                        dgvDeathList.Enabled = false;
                        dataGr.Enabled = false;
                    });
                    countDeathList = 0;
                    updateStatus("Đang thêm vào Death Note ...");
                    base.Invoke((MethodInvoker)delegate
                    {
                        try
                        {
                            dataGr.Sort(dataGr.Columns["fsum"], ListSortDirection.Descending);
                        }
                        catch
                        {
                        }
                    });
                    mainFrm = this;
                    if (checkTotal.Checked == true)
                    {
                        for (int i = dataGr.Rows.Count - 1; i >= 0; i--)
                        {
                            base.Invoke((MethodInvoker)delegate
                            {
                                mainFrm.dataGr.FirstDisplayedScrollingRowIndex = i;
                            });
                            string id = dataGr.Rows[i].Cells[2].Value.ToString();
                            string name = dataGr.Rows[i].Cells[3].Value.ToString();
                            string relationship_status = dataGr.Rows[i].Cells[4].Value.ToString();
                            DateTime age_range = DateTime.Parse(dataGr.Rows[i].Cells[5].Value.ToString());
                            int like = int.Parse(dataGr.Rows[i].Cells[6].Value.ToString());
                            int cmt = int.Parse(dataGr.Rows[i].Cells[7].Value.ToString());
                            int num = int.Parse(dataGr.Rows[i].Cells[8].Value.ToString());
                            if ((bool)dataGr.Rows[i].Cells[0].FormattedValue && ((decimal)num <= numInteractive.Value))
                            {
                                CopyRow(new User(id, name, relationship_status, age_range, like, cmt));
                            }
                        }
                    }
                    else if (checkAll.Checked == true || checkItem.Checked == true)
                    {
                        for (int i = dataGr.Rows.Count - 1; i >= 0; i--)
                        {
                            base.Invoke((MethodInvoker)delegate
                            {
                                mainFrm.dataGr.FirstDisplayedScrollingRowIndex = i;
                            });
                            string id = dataGr.Rows[i].Cells[2].Value.ToString();
                            string name = dataGr.Rows[i].Cells[3].Value.ToString();
                            string relationship_status = dataGr.Rows[i].Cells[4].Value.ToString();
                            DateTime age_range = DateTime.Parse(dataGr.Rows[i].Cells[5].Value.ToString());
                            int like = int.Parse(dataGr.Rows[i].Cells[6].Value.ToString());
                            int cmt = int.Parse(dataGr.Rows[i].Cells[7].Value.ToString());
                            int num = int.Parse(dataGr.Rows[i].Cells[8].Value.ToString());
                            if ((bool)dataGr.Rows[i].Cells[0].FormattedValue)
                            {
                                CopyRow(new User(id, name, relationship_status, age_range, like, cmt));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    FBGraph.Instance.WriteLog(ex.ToString());
                }
                base.Invoke((MethodInvoker)delegate
                {
                    btnUnf.Enabled = true;
                    btnCleanItem.Enabled = true;
                    dgvDeathList.Enabled = true;
                    dataGr.Enabled = true;
                });
                updateStatus("Hoàn thành!");
            }).Start();
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            count = 0;
            var prog = TaskbarManager.Instance;
            prog.SetProgressState(TaskbarProgressBarState.Normal);
            prog.SetProgressValue(0, 0);

            new Thread((ThreadStart)delegate
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                base.Invoke((MethodInvoker)delegate
                {
                    dataGr.Rows.Clear();
                    dataGr.DataSource = null;
                    dataGr.Enabled = false;
                    btnScan.Enabled = false;
                    checkAll.Enabled = false;
                    checkItem.Enabled = false;
                    checkTotal.Enabled = false;
                    btnPrint.Enabled = false;
                    numInteractive.Enabled = false;
                    btnCleanItem.Enabled = false;
                    btnUnf.Enabled = false;
                    //
                    btnPriPost.Enabled = false;
                    btnStatist.Enabled = false;
                    btnPriStatist.Enabled = false;
                    btnAction.Enabled = false;
                    numPost.Enabled = false;
                    cbbAction.Enabled = false;
                });
                try
                {
                    if (string.IsNullOrWhiteSpace(token))
                    {
                        updateStatus("Không thể lấy token! Xin hãy thử lại!");
                        return;
                    }
                    users.Clear();
                    likes.Clear();
                    cmts.Clear();
                    updateStatus("Đang lấy danh sách bạn bè ...");
                    users = FBGraph.Instance.GetFriendUids();
                    if (users.Count == 0)
                    {
                        updateStatus("Bạn bè không tồn tại!");
                        return;
                    }
                    updateStatus("Đang lấy danh sách bài viết, quá trình này sẽ mất vài phút ...");
                    if (post.Count == 0)
                    {
                        post = FBGraph.Instance.GetPosts();
                    }
                    int de = 0;
                    foreach (var item in post)
                    {
                        updateStatus($"Đang thống kê like, cmt: {de}/{post.Count - 1} (ID: {item.id.Split('_')[1]})");
                        foreach (var ite in item.reactions)
                        {
                            if (!likes.Contains(ite))
                            {
                                likes.Add(ite);
                            }
                        }
                        foreach (var ite in item.cmts)
                        {
                            if (!cmts.Contains(ite))
                            {
                                cmts.Add(ite);
                            }
                        }

                        de++;
                        prog.SetProgressValue(de, post.Count - 1);
                    }
                    updateStatus($"Thống kê xong! Đang nhập dữ liệu!");
                    foreach (var ite in users)
                    {
                        ite.likes = likes.Where(x => x.id == ite.id).Count();
                        ite.cmts = cmts.Where(x => x.id == ite.id).Count();
                    }
                    AddRow(users);
                    users.Reverse();
                }
                catch
                {
                    updateStatus("Không thể kết nối dữ liệu!");
                }

                base.Invoke((MethodInvoker)delegate
                {
                    updateStatus("Đang xắp xếp ...");
                    dataGr.Sort(dataGr.Columns[8], ListSortDirection.Descending);
                    dataGr.FirstDisplayedScrollingRowIndex = 0;
                    btnCopy.Enabled = true;
                    btnScan.Enabled = true;
                    checkAll.Enabled = true;
                    checkItem.Enabled = true;
                    checkTotal.Enabled = true;
                    btnPrint.Enabled = true;
                    btnCleanItem.Enabled = true;
                    btnUnf.Enabled = true;
                    dataGr.Enabled = true;
                    //
                    btnPriPost.Enabled = true;
                    btnStatist.Enabled = true;
                    btnPriStatist.Enabled = true;
                    btnAction.Enabled = true;
                    numPost.Enabled = true;
                    cbbAction.Enabled = true;
                    stopWatch.Stop();
                    TimeSpan ts = stopWatch.Elapsed;
                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                        ts.Hours, ts.Minutes, ts.Seconds,
                        ts.Milliseconds / 10);
                    updateStatus($"Hoàn thành! Tổng thời gian: {elapsedTime}.");
                });
            }).Start();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in this.dgvDeathList.SelectedRows)
            {
                dgvDeathList.Rows.RemoveAt(item.Index);
                updateStatus($"Xóa {item.Cells[3].Value.ToString()} khỏi Death Note thành công!");
            }
        }

        private void btnCleanItem_Click(object sender, EventArgs e)
        {
            if (txtId.Text != "")
            {
                for (int i = dgvDeathList.RowCount - 1; i >= 0; i--)
                {
                    if (dgvDeathList.Rows[i].Cells[3].Value.ToString().Contains(txtId.Text))
                    {
                        updateStatus($"Xóa {dgvDeathList.Rows[i].Cells[3].Value.ToString()} ({dgvDeathList.Rows[i].Cells[2].Value.ToString()}) khỏi Death Note thành công!");
                        dgvDeathList.Rows.RemoveAt(i);
                    }
                    else if (dgvDeathList.Rows[i].Cells[2].Value.ToString().Contains(txtId.Text))
                    {
                        updateStatus($"Xóa {dgvDeathList.Rows[i].Cells[3].Value.ToString()} ({dgvDeathList.Rows[i].Cells[2].Value.ToString()}) khỏi Death Note thành công!");
                        dgvDeathList.Rows.RemoveAt(i);
                    }
                }
            }
            else
            {
                for (int i = dgvDeathList.RowCount - 1; i >= 0; i--)
                {
                    if ((bool)dgvDeathList.Rows[i].Cells[0].FormattedValue)
                    {
                        updateStatus($"Xóa {dgvDeathList.Rows[i].Cells[3].Value.ToString()} ({dgvDeathList.Rows[i].Cells[2].Value.ToString()}) khỏi Death Note thành công!");
                        dgvDeathList.Rows.RemoveAt(i);
                    }
                }
                updateStatus("Hoàn thành!");
            }
        }

        private void MainFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
            Application.Exit();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            lb1.Text = Application.ProductVersion;
            if (!TaskbarManager.IsPlatformSupported)
            {
                MessageBox.Show($"Death Click không hỗ trợ cho hệ thống của bạn {Environment.NewLine}. Chỉ hỗ trợ từ windows 7 trở lên.");
                Environment.Exit(Environment.ExitCode);
                Application.Exit();
            }
        }

        private void numInteractive_ValueChanged(object sender, EventArgs e)
        {
            checkTotal.Enabled = true;
        }

        private void MainFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.skin = colorSchemeIndex;
            Settings.Default.Save();
            if (MessageBox.Show("Bạn có muốn thoát khỏi Death click không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            new Thread((ThreadStart)delegate
            {
                base.Invoke((MethodInvoker)delegate
                {
                    btnPrint.Enabled = false;
                });
                DataTable dt = new DataTable();
                foreach (DataGridViewColumn col in dataGr.Columns)
                {
                    dt.Columns.Add(col.HeaderText);
                }

                foreach (DataGridViewRow row in dataGr.Rows)
                {
                    DataRow dRow = dt.NewRow();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        dRow[cell.ColumnIndex] = cell.Value;
                    }
                    dt.Rows.Add(dRow);
                }
                ExportToExcel.Instance.Export(dt, "Danh sach", "DANH SÁCH BẠN BÈ TRÊN FACEBOOK");
                base.Invoke((MethodInvoker)delegate
                {
                    btnPrint.Enabled = true;
                });
            }).Start();
        }

        private void btnStatist_Click(object sender, EventArgs e)
        {
            countPost = 0;
            var prog = TaskbarManager.Instance;
            prog.SetProgressState(TaskbarProgressBarState.Normal);
            prog.SetProgressValue(0, 0);
            new Thread((ThreadStart)delegate
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                base.Invoke((MethodInvoker)delegate
                {
                    dataPost.Rows.Clear();
                    dataPost.DataSource = null;
                    btnScan.Enabled = false;
                    checkAll.Enabled = false;
                    checkItem.Enabled = false;
                    checkTotal.Enabled = false;
                    btnPrint.Enabled = false;
                    numInteractive.Enabled = false;
                    btnCleanItem.Enabled = false;
                    btnUnf.Enabled = false;
                    dataPost.Enabled = false;
                    //
                    btnPriPost.Enabled = false;
                    btnStatist.Enabled = false;
                    btnPriStatist.Enabled = false;
                    btnAction.Enabled = false;
                    numPost.Enabled = false;
                    cbbAction.Enabled = false;
                });
                try
                {
                    if (string.IsNullOrWhiteSpace(token))
                    {
                        updateStatus("Không thể lấy token! Xin hãy thử lại!");
                        return;
                    }

                    updateStatus("Đang lấy danh sách bài viết, quá trình này sẽ mất vài phút ...");
                    //poPost.Clear();
                    int postCount = int.Parse(numPost.Value.ToString());
                    if (post.Count() == 0)
                    {
                        post = FBGraph.Instance.GetPosts();
                    }
                    int dem = 0;
                    foreach (var item in post.Take(postCount))
                    {
                        updateStatus($"Đang thống kê like, cmt: {dem}/{post.Take(postCount).Count()} (ID: {item.id.Split('_')[1]})");
                        dem++;
                        prog.SetProgressValue(dem, post.Take(postCount).Count());
                    }
                    AddRowPost(post, postCount);

                    post.Reverse();
                }
                catch
                {
                    updateStatus("Không thể kết nối dữ liệu!");
                }
                base.Invoke((MethodInvoker)delegate
                {
                    updateStatus("Đang xắp xếp ...");
                    dataPost.Sort(dataPost.Columns[9], ListSortDirection.Descending);
                    dataPost.FirstDisplayedScrollingRowIndex = 0;
                    btnCopy.Enabled = true;
                    btnScan.Enabled = true;
                    checkAll.Enabled = true;
                    checkItem.Enabled = true;
                    checkTotal.Enabled = true;
                    btnPrint.Enabled = true;
                    dataPost.Enabled = true;
                    btnCleanItem.Enabled = true;
                    btnUnf.Enabled = true;

                    //
                    btnPriPost.Enabled = true;
                    btnStatist.Enabled = true;
                    btnPriStatist.Enabled = true;
                    btnAction.Enabled = true;
                    numPost.Enabled = true;
                    cbbAction.Enabled = true;
                    TimeSpan ts = stopWatch.Elapsed;
                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                        ts.Hours, ts.Minutes, ts.Seconds,
                        ts.Milliseconds / 10);
                    updateStatus($"Hoàn thành! Tổng thời gian: {elapsedTime}.");
                });
            }).Start();
        }

        private void btnAction_Click(object sender, EventArgs e)
        {
            int selected = this.cbbAction.SelectedIndex;

            if (this.dataPost.SelectedRows == null)
            {
                updateStatus("Vui lòng chọn bài muốn xem");
            }
            else
            {
                new Thread((ThreadStart)delegate
                {
                    base.Invoke((MethodInvoker)delegate
                    {
                        dataAction.Rows.Clear();
                        dataAction.DataSource = null;
                    });
                    try
                    {
                        countAction = 0;
                        foreach (DataGridViewRow item in this.dataPost.SelectedRows)
                        {
                            if (item.Cells[1].Value.ToString() != null)
                            {
                                foreach (var ite in post.Where(x => x.id == item.Cells[1].Value.ToString()))
                                {
                                    if (selected == 0)
                                    {
                                        HashSet<Comment> cmtpost = new HashSet<Comment>();

                                        foreach (var i in ite.cmts.OrderBy(x => x.created_time))
                                        {
                                            cmtpost.Add(i);
                                        }
                                        foreach (var it in cmtpost.OrderBy(x => x.created_time))
                                        {
                                            AddRowActionCMT(it);
                                        }
                                    }
                                    else
                                    {
                                        HashSet<Reaction> likepost = new HashSet<Reaction>();
                                        likepost.UnionWith(ite.reactions);
                                        foreach (var it in likepost)
                                        {
                                            AddRowActionLike(it);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                updateStatus("Vui lòng chọn bài muốn xem");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        FBGraph.Instance.WriteLog(ex.ToString());
                    }
                }).Start();
            }
        }

        private void AddRowActionLike(Reaction likes)
        {
            DataRow row = dtAction.NewRow();
            countAction++;
            row["fstt"] = countAction;
            row["fid"] = likes.id;
            row["fname"] = likes.name;
            row["ftype"] = likes.type;
            dtAction.Rows.Add(row);
            base.Invoke((MethodInvoker)delegate
            {
                try
                {
                    int dem = dataAction.Rows.Add();
                    dataAction.Rows[dem].Cells[0].Value = row["fstt"];
                    dataAction.Rows[dem].Cells[1].Value = row["fid"].ToString();
                    dataAction.Rows[dem].Cells[2].Value = row["fname"];
                    dataAction.Rows[dem].Cells[3].Value = row["ftype"];

                    dataAction.FirstDisplayedScrollingRowIndex = dem;
                }
                catch (Exception ex)
                {
                    FBGraph.Instance.WriteLog(ex.ToString());
                }
            });
        }

        private void AddRowActionCMT(Comment cmt)
        {
            DataRow row = dtAction.NewRow();
            countAction++;
            row["fstt"] = countAction;
            row["fid"] = cmt.id;
            row["fname"] = cmt.name;
            row["ftype"] = cmt.message;
            dtAction.Rows.Add(row);
            base.Invoke((MethodInvoker)delegate
            {
                try
                {
                    int dem = dataAction.Rows.Add();
                    dataAction.Rows[dem].Cells[0].Value = row["fstt"];
                    dataAction.Rows[dem].Cells[1].Value = row["fid"].ToString();
                    dataAction.Rows[dem].Cells[2].Value = row["fname"];
                    dataAction.Rows[dem].Cells[3].Value = row["ftype"];

                    dataAction.FirstDisplayedScrollingRowIndex = dem;
                }
                catch (Exception ex)
                {
                    FBGraph.Instance.WriteLog(ex.ToString());
                }
            });
        }

        private void btnUserUI_Click(object sender, EventArgs e)
        {
            countUser = 0;
            var prog = TaskbarManager.Instance;
            prog.SetProgressState(TaskbarProgressBarState.Indeterminate);
            prog.SetProgressValue(0, 0);
            if (post.Count() > 0)
            {
                new Thread((ThreadStart)delegate
                {
                    Stopwatch stopWatch = new Stopwatch();
                    stopWatch.Start();
                    base.Invoke((MethodInvoker)delegate
                    {
                        dataUserUI.Rows.Clear();
                        dataUserUI.DataSource = null;
                        btnPriStatist.Enabled = false;
                    });
                    try
                    {
                        int postCount = int.Parse(numPost.Value.ToString());
                        if (post.Take(postCount).Count() > 0)
                        {
                            updateStatus($"Đang thống kê của {postCount} bài post!");
                            HashSet<Post> newpo = new HashSet<Post>();
                            if (newpo.Count() > 0)
                            {
                                newpo.Clear();
                            }
                            newpo.UnionWith(post.Take(postCount));
                            usersui.Clear();
                            lus.Clear();
                            cus.Clear();
                            foreach (var item in getListUser(newpo))
                            {
                                usersui.Add(new User(item.id, item.name, item.relationship_status, item.age_range));
                            }

                            int de = 0;
                            foreach (var item in post.Take(postCount))
                            {
                                updateStatus($"Đang thống kê like, cmt: {de}/{post.Take(postCount).Count()} (ID: {item.id.Split('_')[1]})");
                                foreach (var ite in item.reactions)
                                {
                                    if (!lus.Contains(ite))
                                    {
                                        lus.Add(ite);
                                    }
                                }
                                foreach (var ite in item.cmts)
                                {
                                    if (!cus.Contains(ite))
                                    {
                                        cus.Add(ite);
                                    }
                                }
                                prog.SetProgressValue(de, post.Take(postCount).Count());
                                de++;
                            }
                            foreach (var item in usersui)
                            {
                                item.likes = lus.Where(x => x.id == item.id).Count();
                                item.cmts = cus.Where(x => x.id == item.id).Count();
                            }
                            updateStatus($"Đang cập nhật thông tin dữ liệu!");
                            AddRowUser(usersui);
                            usersui.Reverse();
                        }
                        else
                        {
                            updateStatus("Thống kê bài viết trước!");
                        }
                    }
                    catch (Exception ex)
                    {
                        FBGraph.Instance.WriteLog(ex.ToString());
                    }
                    base.Invoke((MethodInvoker)delegate
                    {
                        updateStatus("Sắp xếp tài khoản!");
                        dataUserUI.Sort(dataUserUI.Columns[5], ListSortDirection.Descending);
                        TimeSpan ts = stopWatch.Elapsed;
                        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                            ts.Hours, ts.Minutes, ts.Seconds,
                            ts.Milliseconds / 10);
                        updateStatus($"Hoàn thành! Tổng thời gian: {elapsedTime}.");
                        btnPriStatist.Enabled = true;
                    });
                }).Start();
            }
            else
            {
                updateStatus("Không tồn tại dữ liệu, vui lòng thống kê trước!");
            }
        }

        private void AddRowUser(HashSet<User> user)
        {
            foreach (var item in user)
            {
                DataRow row = dtUser.NewRow();
                countUser++;
                row["fstt"] = countUser;
                row["fid"] = item.id;
                row["fname"] = item.name;
                row["flike"] = item.likes;
                row["fcmt"] = item.cmts;
                row["fsum"] = item.sum;
                dtUser.Rows.Add(row);
                base.Invoke((MethodInvoker)delegate
                {
                    try
                    {
                        int dem = dataUserUI.Rows.Add();
                        dataUserUI.Rows[dem].Cells[0].Value = int.Parse(row["fstt"].ToString());
                        dataUserUI.Rows[dem].Cells[1].Value = row["fid"].ToString();
                        dataUserUI.Rows[dem].Cells[2].Value = row["fname"];
                        dataUserUI.Rows[dem].Cells[3].Value = int.Parse(row["flike"].ToString());
                        dataUserUI.Rows[dem].Cells[4].Value = int.Parse(row["fcmt"].ToString());
                        dataUserUI.Rows[dem].Cells[5].Value = int.Parse(row["fsum"].ToString());
                        dataUserUI.FirstDisplayedScrollingRowIndex = dem;
                    }
                    catch (Exception ex)
                    {
                        FBGraph.Instance.WriteLog(ex.ToString());
                    }
                });
            }
        }

        private HashSet<IUserFB> getListUser(HashSet<Post> po)
        {
            HashSet<IUserFB> iu = new HashSet<IUserFB>();
            foreach (var item in po)
            {
                foreach (var ite in item.reactions)
                {
                    if (iu.Where(x => x.id == ite.id).Count() > 0)
                    {
                    }
                    else
                    {
                        iu.Add(ite);
                    }
                }
                foreach (var ite in item.cmts)
                {
                    if (iu.Where(x => x.id == ite.id).Count() > 0)
                    {
                    }
                    else
                    {
                        iu.Add(ite);
                    }
                }
            }
            return iu;
        }

        private void btnPriStatist_Click(object sender, EventArgs e)
        {
            new Thread((ThreadStart)delegate
            {
                base.Invoke((MethodInvoker)delegate
                {
                    btnPriStatist.Enabled = false;
                });
                if (dataUserUI.RowCount > 0)
                {
                    DataTable dt = new DataTable();
                    foreach (DataGridViewColumn col in dataUserUI.Columns)
                    {
                        dt.Columns.Add(col.HeaderText);
                    }

                    foreach (DataGridViewRow row in dataUserUI.Rows)
                    {
                        DataRow dRow = dt.NewRow();
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            dRow[cell.ColumnIndex] = cell.Value;
                        }
                        dt.Rows.Add(dRow);
                    }
                    ExportToExcel.Instance.Export(dt, "Danh sach", "DANH SÁCH TÀI KHOẢN TRÊN FACEBOOK");
                }

                base.Invoke((MethodInvoker)delegate
                {
                    btnPriStatist.Enabled = true;
                });
            }).Start();
        }

        private void numPost_ValueChanged(object sender, EventArgs e)
        {
            countPost = 0;
            var prog = TaskbarManager.Instance;
            prog.SetProgressState(TaskbarProgressBarState.Normal);
            prog.SetProgressValue(0, 0);
            if (post.Count() > 0)
            {
                new Thread((ThreadStart)delegate
                {
                    Stopwatch stopWatch = new Stopwatch();
                    stopWatch.Start();
                    base.Invoke((MethodInvoker)delegate
                    {
                        dataPost.Rows.Clear();
                        dataPost.DataSource = null;
                    });
                    try
                    {
                        updateStatus("Thống kê danh sách bài viết ...");
                        int postCount = int.Parse(numPost.Value.ToString());
                        updateStatus($"Đang cập nhật thông tin dữ liệu!");
                        int dem = 0;
                        foreach (var item in post.Take(postCount))
                        {
                            updateStatus($"Đang thống kê like, cmt: {dem}/{post.Take(postCount).Count()} (ID: {item.id.Split('_')[1]})");
                            dem++;

                            prog.SetProgressValue(dem, post.Take(postCount).Count());
                        }
                        AddRowPost(post, postCount);
                        post.Reverse();
                    }
                    catch
                    {
                        updateStatus("Không thể kết nối dữ liệu!");
                    }
                    base.Invoke((MethodInvoker)delegate
                    {
                        updateStatus("Đang xắp xếp ...");
                        dataPost.Sort(dataPost.Columns[9], ListSortDirection.Descending);
                        TimeSpan ts = stopWatch.Elapsed;
                        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                            ts.Hours, ts.Minutes, ts.Seconds,
                            ts.Milliseconds / 10);
                        updateStatus($"Hoàn thành! Tổng thời gian: {elapsedTime}.");
                    });
                }).Start();
            }
            else
            {
                updateStatus("Không tồn tại dữ liệu, vui lòng thống kê trước!");
            }
        }

        private void btnPriPost_Click(object sender, EventArgs e)
        {
            new Thread((ThreadStart)delegate
            {
                base.Invoke((MethodInvoker)delegate
                {
                    btnPriPost.Enabled = false;
                });
                if (dataPost.RowCount > 0)
                {
                    DataTable dt = new DataTable();
                    foreach (DataGridViewColumn col in dataPost.Columns)
                    {
                        dt.Columns.Add(col.HeaderText);
                    }

                    foreach (DataGridViewRow row in dataPost.Rows)
                    {
                        DataRow dRow = dt.NewRow();
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            dRow[cell.ColumnIndex] = cell.Value;
                        }
                        dt.Rows.Add(dRow);
                    }
                    ExportToExcel.Instance.Export(dt, "Danh sach", "DANH SÁCH BÀI ĐĂNG TRÊN FACEBOOK");
                }

                base.Invoke((MethodInvoker)delegate
                {
                    btnPriPost.Enabled = true;
                });
            }).Start();
        }

        private void btnSkin_Click(object sender, EventArgs e)
        {
            colorSchemeIndex++;
            if (colorSchemeIndex == 8)
            {
                colorSchemeIndex = 0;
            }
            SetColor(colorSchemeIndex);
        }

        #endregion Sự kiện
    }
}