using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BetManager
{
    public partial class Form1 : Form
    {
        private enum DGV_LIST {
            No,
            Odds
        }
        List<int> min_profit = new List<int>();
        List<int> max_profit = new List<int>();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            dgvList.Rows.Add("", "");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            dgvList.Rows.Remove(dgvList.CurrentRow);
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            float castable;
            int trails = 0;
            int sum_bet = 0;
            Boolean flag = false;
            int[] bet;
            float[] odds;
            float[] refund; ;
            DataGridViewTextBoxColumn textColumn;
            try
            {
                while (dgvList.Columns.Count > 2)
                {
                    dgvList.Columns.RemoveAt(2);
                }
                for (int i=0; i<dgvList.Rows.Count; i++)
                {
                    if (dgvList.Rows[i].Cells[0].Value.ToString().Equals("min"))
                    {
                        dgvList.Rows.RemoveAt(i);
                    }
                    if (dgvList.Rows[i].Cells[0].Value.ToString().Equals("max"))
                    {
                        dgvList.Rows.RemoveAt(i);
                    }
                }
                odds = new float[dgvList.Rows.Count];
                refund = new float[dgvList.Rows.Count];
                for (int i=0; i < dgvList.Rows.Count; i++)
                {
                    if (dgvList.Rows[i].Cells[(int)DGV_LIST.No].Value.ToString().Trim() == "")
                    {
                        throw new Exception("There is a blank field in No ");
                    }
                    if (dgvList.Rows[i].Cells[(int)DGV_LIST.Odds].Value.ToString().Trim() == "")
                    {
                        throw new Exception("There is a blank field in Odds");
                    }
                    if (float.TryParse(dgvList.Rows[i].Cells[(int)DGV_LIST.Odds].Value.ToString(), out castable))
                    {
                    }
                    else
                    {
                        throw new Exception("There is a string field in Odds");
                    }
                }
                dgvList.Sort(dgvList.Columns[(int)DGV_LIST.Odds], System.ComponentModel.ListSortDirection.Ascending);
                trails = int.Parse(txtNumberOfTrails.Text.Trim());
                bet = new int[dgvList.Rows.Count];
                for (int i=0; i<dgvList.Rows.Count; i++)
                {
                    odds[i] = float.Parse(dgvList.Rows[i].Cells[1].Value.ToString());
                    bet[i] = 1;
                }

                for (int i=0; i<trails; i++)
                {
                    textColumn = new DataGridViewTextBoxColumn();
                    textColumn.Name = "col" + i;
                    textColumn.HeaderText = (i+1).ToString();
                    dgvList.Columns.Add(textColumn);
                    sum_bet = 0;
                    
                    for(int j=0; j<dgvList.Rows.Count; j++)
                    {
                        refund[j] = 0;
                    }

                    for(int j=0; j<dgvList.Rows.Count; j++)
                    {
                        dgvList.Rows[j].Cells[i + 2].Value = bet[j];
                        refund[j] = bet[j] * odds[j] * 100;
                        sum_bet += bet[j] * 100;
                    }

                    min_profit.Add((int)(refund.Min() - sum_bet + 0.5));
                    max_profit.Add((int)(refund.Max() - sum_bet + 0.5));

                    flag = true;
                    for(int j=0; j<dgvList.Rows.Count; j++)
                    {
                        if(refund[j] < sum_bet)
                        {
                            bet[j] += 1;
                            flag = false;
                        }
                    }
                    if (flag == true)
                    {
                        for (int k = 0; k < dgvList.Rows.Count; k++)
                        {
                            bet[k] += 1;
                        }
                    }
                }

                for (int i = 0; i < dgvList.Rows.Count; i++)
                {
                    bet[i] = int.Parse(dgvList.Rows[i].Cells[dgvList.Columns.Count - 1].Value.ToString());
                    refund[i] = bet[i] * odds[i] * 100;
                    sum_bet += bet[i] * 100;
                }
                min_profit.Add((int)(refund.Min() - sum_bet + 0.5));
                max_profit.Add((int)(refund.Max() - sum_bet + 0.5));

                dgvList.Rows.Add();
                for (int i=2; i<dgvList.Columns.Count ; i++)
                {
                    dgvList.Rows[dgvList.Rows.Count - 1].Cells[i].Value = min_profit[i - 2];
                }
                dgvList.Rows.Add();
                for (int i=2; i < dgvList.Columns.Count; i++)
                {
                    dgvList.Rows[dgvList.Rows.Count - 1].Cells[i].Value = max_profit[i - 2];
                }

                dgvList.Rows[dgvList.Rows.Count - 2].Cells[0].Value = "min";
                dgvList.Rows[dgvList.Rows.Count - 2].Cells[1].Value = "";
                dgvList.Rows[dgvList.Rows.Count - 1].Cells[0].Value = "max";
                dgvList.Rows[dgvList.Rows.Count - 1].Cells[1].Value = "";

                for(int i=2; i < dgvList.Columns.Count; i++)
                {
                    if(int.Parse(dgvList.Rows[dgvList.Rows.Count - 2].Cells[i].Value.ToString()) >= 0)
                    {
                        dgvList.Columns[i].DefaultCellStyle.BackColor = Color.LightBlue;
                    }
                }
                max_profit.Clear();
                min_profit.Clear();
            } 
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //testData
            dgvList.Rows.Add(10, 5.3);
            dgvList.Rows.Add(15, 2.1);
            dgvList.Rows.Add(13, 11.8);
            dgvList.Rows.Add(4, 12.1);

        }
    }
}
