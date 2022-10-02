using Formula;

namespace Formula
{

    /// <summary>
    /// 测试
    /// </summary>
    public partial class FormulaTest : Form
    {
        public FormulaTest()
        {
            InitializeComponent();
        }

        private void FormulaTest_Load(object sender, EventArgs e)
        {
            foreach (var item in Enum.GetValues(typeof(FormulaVersion)))
            {
                this.cmbVersion.Items.Add(item);
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            FormulaVersion enVersion = (FormulaVersion)this.cmbVersion.SelectedItem;

            switch (enVersion)
            {
                case FormulaVersion.FV10:
                    TEST_V10();
                    break;
                case FormulaVersion.None:
                    break;
                default:
                    break;
            }
        }


        public void TEST_V10()
        {
            string strFormula = string.Empty;
            string strResult = string.Empty;
            double dResult = 0.0;

            //
            TEST_Version(FormulaVersion.FV10);

            //空
            strFormula = "  ";
            dResult = 0;
            TEST_Item(strFormula, dResult, Formula.Calculate(strFormula, FormulaVersion.FV10));

            //单字符
            strFormula = " 0 ";
            dResult = 0;
            TEST_Item(strFormula, dResult, Formula.Calculate(strFormula, FormulaVersion.FV10));
            strFormula = " - 0 ";
            dResult = -0;
            TEST_Item(strFormula, dResult, Formula.Calculate(strFormula, FormulaVersion.FV10));

            //单运算
            strFormula = " 0- 1 ";
            dResult = 0 - 1;
            TEST_Item(strFormula, dResult, Formula.Calculate(strFormula, FormulaVersion.FV10));
            strFormula = " 2 /3.0 ";
            dResult = 2 / 3.0;
            TEST_Item(strFormula, dResult, Formula.Calculate(strFormula, FormulaVersion.FV10));

            //双运算
            strFormula = " 0- 1+ 3 ";
            dResult = 0 - 1 + 3;
            TEST_Item(strFormula, dResult, Formula.Calculate(strFormula, FormulaVersion.FV10));
            strFormula = " 2 /3.0 *4";
            dResult = 2 / 3.0 * 4;
            TEST_Item(strFormula, dResult, Formula.Calculate(strFormula, FormulaVersion.FV10));

            //混合双运算
            strFormula = " -0* 1+ 3 ";
            dResult = -0 * 1 + 3;
            TEST_Item(strFormula, dResult, Formula.Calculate(strFormula, FormulaVersion.FV10));
            strFormula = " 2 /3.0 --4";
            dResult = 2 / 3.0 - -4;
            TEST_Item(strFormula, dResult, Formula.Calculate(strFormula, FormulaVersion.FV10));

            //三运算
            strFormula = "1 -0- 1+ 3 ";
            dResult = 1 - 0 - 1 + 3;
            TEST_Item(strFormula, dResult, Formula.Calculate(strFormula, FormulaVersion.FV10));
            strFormula = "1* 2 /3 /+4";
            dResult = 1 * 2 / 3 / +4;
            TEST_Item(strFormula, dResult, Formula.Calculate(strFormula, FormulaVersion.FV10));

            //混合三运算
            strFormula = "1 -0/ 1+ -3 ";
            dResult = 1 - 0 / 1 + -3;
            TEST_Item(strFormula, dResult, Formula.Calculate(strFormula, FormulaVersion.FV10));
            strFormula = "-1* 2 -3 /-4";
            dResult = -1 * 2 - 3 / -4;
            TEST_Item(strFormula, dResult, Formula.Calculate(strFormula, FormulaVersion.FV10));

            //四运算
            strFormula = "1 -0- 1+ 3* 4/5.6 ";
            dResult = 1 - 0 - 1 + 3 * 4 / 5.6;
            TEST_Item(strFormula, dResult, Formula.Calculate(strFormula, FormulaVersion.FV10));
            strFormula = "1 /2 *3  +4-+5";
            dResult = 1 / 2 * 3 + 4 - +5;
            TEST_Item(strFormula, dResult, Formula.Calculate(strFormula, FormulaVersion.FV10));

            //混合四运算
            strFormula = "-1/ -3+ 1 -0 * 4 ";
            dResult = -1 / -3 + 1 - 0 * 4;
            TEST_Item(strFormula, dResult, Formula.Calculate(strFormula, FormulaVersion.FV10));
            strFormula = "+0--1* 2 /-3.5 +-4";
            dResult = +0 - -1 * 2 / -3.5 + -4;
            TEST_Item(strFormula, dResult, Formula.Calculate(strFormula, FormulaVersion.FV10));

            //混合运算
            strFormula = "0-1/ -3+ 1 -0 * 4 /9*8.7/7.7-6.5";
            dResult = 0 - 1 / -3 + 1 - 0 * 4 / 9 * 8.7 / 7.7 - 6.5;
            TEST_Item(strFormula, dResult, Formula.Calculate(strFormula, FormulaVersion.FV10));
            strFormula = "1*0.0-1- -3+ 1 -0 * 4 /9*8.7/7.7-6.5+0--1* 2 /-3.5 +-4/4.0";
            dResult = 1 * 0.0 - 1 - -3 + 1 - 0 * 4 / 9 * 8.7 / 7.7 - 6.5 + 0 - -1 * 2 / -3.5 + -4 / 4.0;
            TEST_Item(strFormula, dResult, Formula.Calculate(strFormula, FormulaVersion.FV10));


        }


        private void TEST_Item(string strFormula, double dResult, string strResult)
        {
            //
            txtTestLog.Text += Environment.NewLine;
            txtTestLog.Text += strFormula;
            txtTestLog.Text += Environment.NewLine;
            txtTestLog.Text += string.Format("[{0}] <=> [{1}]", dResult, strResult);
            txtTestLog.Text += Environment.NewLine;

        }
        private void TEST_Version(FormulaVersion enVersion)
        {
            //
            txtTestLog.Text += Environment.NewLine;
            txtTestLog.Text += string.Format("---------{0}---------", enVersion.ToString());
            txtTestLog.Text += Environment.NewLine;
            txtTestLog.Text += Environment.NewLine;

        }
    }
}