using Formula;

namespace Formula
{

    /// <summary>
    /// ????
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
                case FormulaVersion.FV12:
                    TEST_V10();
                    TEST_V11();
                    TEST_V12();
                    break;
                case FormulaVersion.FV11:
                    TEST_V10();
                    TEST_V11();
                    break;
                case FormulaVersion.FV10:
                    TEST_V10();
                    break;
                case FormulaVersion.None:
                    break;
                default:
                    break;
            }
        }


        public void TEST_V12()
        {
            string strFormula = string.Empty;
            string strResult = string.Empty;
            double dResult = 0.0;

            //
            TEST_Version(FormulaVersion.FV12);
            Formula oFormula = Formula.Calculate(FormulaVersion.FV12);

            //??????????
            strFormula = " - ( 1e-2) ";
            dResult = -(1e-2);
            TEST_Item(strFormula, dResult, oFormula.Calculate(strFormula));
            strFormula = " +(-3e-4 )";
            dResult = +(-3e-4);
            TEST_Item(strFormula, dResult, oFormula.Calculate(strFormula));
            strFormula = " - (-2 * -1e+3) ";
            dResult = -(-2 * -1e+3);
            TEST_Item(strFormula, dResult, oFormula.Calculate(strFormula));
            strFormula = " +(3.2 +-3e-4 )";
            dResult = +(3.2 + -3e-4);
            TEST_Item(strFormula, dResult, oFormula.Calculate(strFormula));

            //˫????????
            strFormula = " (- (+2-0 * 3/4)) ";
            dResult = (-(+2 - 0 * 3 / 4));
            TEST_Item(strFormula, dResult, oFormula.Calculate(strFormula));
            strFormula = "- (  +(-5/+2*0.3 + -3 - - 4)) ";
            dResult = -(+(-5 / +2 * 0.3 + -3 - -4));
            TEST_Item(strFormula, dResult, oFormula.Calculate(strFormula));
            strFormula = " (+(-2 * 1e0 /3.2 + - 4 +5)) ";
            dResult = (+(-2 * 1e0 / 3.2 + -4 + 5));
            TEST_Item(strFormula, dResult, oFormula.Calculate(strFormula));
            strFormula = "+ (+(+4-2 * 1e0 /3.2 + - 4 +5/6e-3)) ";
            dResult = +(+(+4 - 2 * 1e0 / 3.2 + -4 + 5 / 6e-3));
            TEST_Item(strFormula, dResult, oFormula.Calculate(strFormula));

            //????????????
            strFormula = " -(0.5 *+7.9/ - 3 e-2 ) --(- (+2-0 * 3/4)) ";
            dResult = -(0.5 * +7.9 / -3e-2) - -(-(+2 - 0 * 3 / 4));
            TEST_Item(strFormula, dResult, oFormula.Calculate(strFormula));
            strFormula = "-0.5 /-(3.4/ -0.3 + +(+0.5) *+7.9/ - 3e-2 ) ++(- (+2-0 * 3/4) -0 +6 ) * 3.3 ";
            dResult = -0.5 / -(3.4 / -0.3 + +(+0.5) * +7.9 / -3e-2) + +(-(+2 - 0 * 3 / 4) - 0 + 6) * 3.3;
            TEST_Item(strFormula, dResult, oFormula.Calculate(strFormula));
            strFormula = "2+- 1--(-0 + -(0.5 *+7.9/ - 3e2 )+-( -1E-2*-( 2e01 - +4.5)* 6.7)-+(- (+2-0 * 3/4)))+7.6--8.9 ";
            dResult = 2 + -1 - -(-0 + -(0.5 * +7.9 / -3e2) + -(-1E-2 * -(2e01 - +4.5) * 6.7) - +(-(+2 - 0 * 3 / 4))) + 7.6 - -8.9;
            TEST_Item(strFormula, dResult, oFormula.Calculate(strFormula));
            strFormula = "2+- 1/-(-0 + -(0.5 *+7.9/ - 3e2 )*-( -1E-2*-( 2e01 - +4.5)* 6.7)/+(- (+2-0 * 3/4)))+7.6--8.9 ";
            dResult = 2 + -1 / -(-0 + -(0.5 * +7.9 / -3e2) * -(-1E-2 * -(2e01 - +4.5) * 6.7) / +(-(+2 - 0 * 3 / 4))) + 7.6 - -8.9;
            TEST_Item(strFormula, dResult, oFormula.Calculate(strFormula));

        }

        public void TEST_V11()
        {
            string strFormula = string.Empty;
            string strResult = string.Empty;
            double dResult = 0.0;

            //
            TEST_Version(FormulaVersion.FV11);
            Formula oFormula = Formula.Calculate(FormulaVersion.FV11);

            //??ѧ??
            strFormula = " 0 e2 ";
            dResult = 0e2;
            TEST_Item(strFormula, dResult, oFormula.Calculate(strFormula));
            strFormula = " 1e-3 ";
            dResult = 1e-3;
            TEST_Item(strFormula, dResult, oFormula.Calculate(strFormula));
            strFormula = " 1e+4 ";
            dResult = 1e+4;
            TEST_Item(strFormula, dResult, oFormula.Calculate(strFormula));

            //?з??ſ?ѧ??
            strFormula = "- 1e5 ";
            dResult = -1e5;
            TEST_Item(strFormula, dResult, oFormula.Calculate(strFormula));
            strFormula = "+ 1e- 6  ";
            dResult = +1e-6;
            TEST_Item(strFormula, dResult, oFormula.Calculate(strFormula));
            strFormula = " -1e +7 ";
            dResult = -1e+7;
            TEST_Item(strFormula, dResult, oFormula.Calculate(strFormula));

            //????
            strFormula = "(- 0 ) ";
            dResult = (-0);
            TEST_Item(strFormula, dResult, oFormula.Calculate(strFormula));
            strFormula = " (- 1e0 )";
            dResult = (-1e0);
            TEST_Item(strFormula, dResult, oFormula.Calculate(strFormula));

            //???ŵ?????
            strFormula = "(-1- 0 ) ";
            dResult = (-1 - 0);
            TEST_Item(strFormula, dResult, oFormula.Calculate(strFormula));
            strFormula = " (1.2/ - 3e04 )";
            dResult = (1.2 / -3e04);
            TEST_Item(strFormula, dResult, oFormula.Calculate(strFormula));

            //????˫????
            strFormula = " (+ 2+- 1- -0 ) ";
            dResult = (+2 + -1 - -0);
            TEST_Item(strFormula, dResult, oFormula.Calculate(strFormula));
            strFormula = " (-0.5 *+1.2/ - 3 e04 )";
            dResult = (-0.5 * +1.2 / -3e04);
            TEST_Item(strFormula, dResult, oFormula.Calculate(strFormula));

            //???Ż???????
            strFormula = " (-0.5 *+1.2/ - 3 e04 + 2+- 1- -0 +6 ) ";
            dResult = (-0.5 * +1.2 / -3e04 + 2 + -1 - -0 + 6) ;
            TEST_Item(strFormula, dResult, oFormula.Calculate(strFormula));
            strFormula = " (6.7++7.6--8.9-0.5 *+7.9/ - 3 e-2 )";
            dResult = (6.7 + +7.6 - -8.9 - 0.5 * +7.9 / -3e-2);
            TEST_Item(strFormula, dResult, oFormula.Calculate(strFormula));
            strFormula = " (-0.5 *+1.2/ - 3 e04 + 2+- 1- -0 +6 *6.7++7.6--8.9-0.5 *+7.9/ - 3 e-2 )";
            dResult = (-0.5 * +1.2 / -3e04 + 2 + -1 - -0 + 6 * 6.7 + +7.6 - -8.9 - 0.5 * +7.9 / -3e-2);
            TEST_Item(strFormula, dResult, oFormula.Calculate(strFormula));
            strFormula = " (+6.7++7.6--8.9-0.5 *+7.9/ - 3 e-2 / -0.5 *+1.2/ - 3 e04 + 2+- 1- -0 +6 )";
            dResult = (+6.7 + +7.6 - -8.9 - 0.5 * +7.9 / -3e-2 / -0.5 * +1.2 / -3e04 + 2 + -1 - -0 + 6);
            TEST_Item(strFormula, dResult, oFormula.Calculate(strFormula));

            //?????ŵ?????
            strFormula = "((- 0 ) ++2.3)";
            dResult = ((-0) + +2.3);
            TEST_Item(strFormula, dResult, oFormula.Calculate(strFormula));
            strFormula = "( ( - 2e01 )/ +4.5)";
            dResult = ((-2e01) / +4.5);
            TEST_Item(strFormula, dResult, oFormula.Calculate(strFormula));
            strFormula = " (-1.2-(- 3 ) )";
            dResult = (-1.2 - (-3));
            TEST_Item(strFormula, dResult, oFormula.Calculate(strFormula));
            strFormula = "(2E4/ ( + 3e01 ))";
            dResult = (2E4 / (+3e01));
            TEST_Item(strFormula, dResult, oFormula.Calculate(strFormula));

            //??????˫????
            strFormula = "((+1- 0 ) ++2.3--4E-5)";
            dResult = ((+1 - 0) + +2.3 - -4E-5);
            TEST_Item(strFormula, dResult, oFormula.Calculate(strFormula));
            strFormula = "( (-1E-2* - 2e01 )/ +4.5* 6.7)";
            dResult = ((-1E-2 * -2e01) / +4.5 * 6.7);
            TEST_Item(strFormula, dResult, oFormula.Calculate(strFormula));
            strFormula = " (-1.2-(- 3 -+4E5) +- 5E6 )";
            dResult = (-1.2 - (-3 - +4E5) + -5E6);
            TEST_Item(strFormula, dResult, oFormula.Calculate(strFormula));
            strFormula = "(+9*( -2E4/ + 3e01 ) /+5.2) ";
            dResult = (+9 * (-2E4 / +3e01) / +5.2);
            TEST_Item(strFormula, dResult, oFormula.Calculate(strFormula));

            //????????β????
            strFormula = "((+1- 0 ) ++2.3--4E-5)++7.6--8.9";
            dResult = ((+1 - 0) + +2.3 - -4E-5) + +7.6 - -8.9;
            TEST_Item(strFormula, dResult, oFormula.Calculate(strFormula));
            strFormula = "- 1- -0 +((+1- 0 ) ++2.3--4E-5)";
            dResult = -1 - -0 + ((+1 - 0) + +2.3 - -4E-5);
            TEST_Item(strFormula, dResult, oFormula.Calculate(strFormula));
            strFormula = " ( (+1* 0.2 /+3) /+2.3*-4E-5 )/+7.6*-8.9 ";
            dResult = ((+1 * 0.2 / +3) / +2.3 * -4E-5) / +7.6 * -8.9;
            TEST_Item(strFormula, dResult, oFormula.Calculate(strFormula));
            strFormula = "- 1* -0.5 /((3.4/+1*- 1E2 ) *+2.3/-4E-5)";
            dResult = -1 * -0.5 / ((3.4 / +1 * -1E2) * +2.3 / -4E-5);
            TEST_Item(strFormula, dResult, oFormula.Calculate(strFormula));

            //???????Ż???????
            strFormula = "- 1--0.5 /(3.4/ -0.3 +((+1- 0.3  *+2.3)--4E-5)*-4E-5 )/+7.6*-8.9 ";
            dResult = -1 - -0.5 / (3.4 / -0.3 + ((+1 - 0.3 * +2.3) - -4E-5) * -4E-5) / +7.6 * -8.9;
            TEST_Item(strFormula, dResult, oFormula.Calculate(strFormula));
            strFormula = "+2.3*-4E-5 -( 1*-0.5 -(3.4/ -0.4 +(+1- 0.3  *+2.3)+-4E-5)*-4E-5 )/+7.6- 3e04 + 2 ";
            dResult = +2.3 * -4E-5 - (1 * -0.5 - (3.4 / -0.4 + (+1 - 0.3 * +2.3) + -4E-5) * -4E-5) / +7.6 - 3e04 + 2;
            TEST_Item(strFormula, dResult, oFormula.Calculate(strFormula));
            strFormula = "-0.5 + - 3e-2 / (-0.5  -4E-5 -( (+1* 0.2 /+3-(3.4/+1*- 1E2  *+2.3)) /+2.3*-4E-5)) /+7.6*-8.9 ";
            dResult = -0.5 + -3e-2 / (-0.5 - 4E-5 - ((+1 * 0.2 / +3 - (3.4 / +1 * -1E2 * +2.3)) / +2.3 * -4E-5)) / +7.6 * -8.9;
            TEST_Item(strFormula, dResult, oFormula.Calculate(strFormula));
            strFormula = "+2.3*-4E-5 -( 1*-0.5 -(3.4/ -0.6 +(+1- 0.3  *+2.3)+-4E-5)*-4E-5 )/+7.6- 3e04 + 2-0.5 + - 3e-2 / (-0.5  -4E-5 -( (+1* 0.2 /+3-(3.4/+1*- 1E2  *+2.3)) /+2.3*-4E-5)) -+7.6*-8.9 ";
            dResult = +2.3 * -4E-5 - (1 * -0.5 - (3.4 / -0.6 + (+1 - 0.3 * +2.3) + -4E-5) * -4E-5) / +7.6 - 3e04 + 2 - 0.5 + -3e-2 / (-0.5 - 4E-5 - ((+1 * 0.2 / +3 - (3.4 / +1 * -1E2 * +2.3)) / +2.3 * -4E-5)) - +7.6 * -8.9;
            TEST_Item(strFormula, dResult, oFormula.Calculate(strFormula));

        }

        public void TEST_V10()
        {
            string strFormula = string.Empty;
            string strResult = string.Empty;
            double dResult = 0.0;

            //
            TEST_Version(FormulaVersion.FV10);

            //??
            strFormula = "  ";
            dResult = 0;
            TEST_Item(strFormula, dResult, Formula.Calculate(strFormula, FormulaVersion.FV10));

            //???ַ?
            strFormula = " 0 ";
            dResult = 0;
            TEST_Item(strFormula, dResult, Formula.Calculate(strFormula, FormulaVersion.FV10));
            strFormula = " - 0 ";
            dResult = -0;
            TEST_Item(strFormula, dResult, Formula.Calculate(strFormula, FormulaVersion.FV10));

            //??????
            strFormula = " 0- 1 ";
            dResult = 0 - 1;
            TEST_Item(strFormula, dResult, Formula.Calculate(strFormula, FormulaVersion.FV10));
            strFormula = " 2 /3.0 ";
            dResult = 2 / 3.0;
            TEST_Item(strFormula, dResult, Formula.Calculate(strFormula, FormulaVersion.FV10));

            //˫????
            strFormula = " 0- 1+ 3 ";
            dResult = 0 - 1 + 3;
            TEST_Item(strFormula, dResult, Formula.Calculate(strFormula, FormulaVersion.FV10));
            strFormula = " 2 /3.0 *4";
            dResult = 2 / 3.0 * 4;
            TEST_Item(strFormula, dResult, Formula.Calculate(strFormula, FormulaVersion.FV10));

            //????˫????
            strFormula = " -0* 1+ 3 ";
            dResult = -0 * 1 + 3;
            TEST_Item(strFormula, dResult, Formula.Calculate(strFormula, FormulaVersion.FV10));
            strFormula = " 2 /3.0 --4";
            dResult = 2 / 3.0 - -4;
            TEST_Item(strFormula, dResult, Formula.Calculate(strFormula, FormulaVersion.FV10));

            //??????
            strFormula = "1 -0- 1+ 3 ";
            dResult = 1 - 0 - 1 + 3;
            TEST_Item(strFormula, dResult, Formula.Calculate(strFormula, FormulaVersion.FV10));
            strFormula = "1* 2 /3 /+4";
            dResult = 1 * 2 / 3 / +4;
            TEST_Item(strFormula, dResult, Formula.Calculate(strFormula, FormulaVersion.FV10));

            //??????????
            strFormula = "1 -0/ 1+ -3 ";
            dResult = 1 - 0 / 1 + -3;
            TEST_Item(strFormula, dResult, Formula.Calculate(strFormula, FormulaVersion.FV10));
            strFormula = "-1* 2 -3 /-4";
            dResult = -1 * 2 - 3 / -4;
            TEST_Item(strFormula, dResult, Formula.Calculate(strFormula, FormulaVersion.FV10));

            //??????
            strFormula = "1 -0- 1+ 3* 4/5.6 ";
            dResult = 1 - 0 - 1 + 3 * 4 / 5.6;
            TEST_Item(strFormula, dResult, Formula.Calculate(strFormula, FormulaVersion.FV10));
            strFormula = "1 /2 *3  +4-+5";
            dResult = 1 / 2 * 3 + 4 - +5;
            TEST_Item(strFormula, dResult, Formula.Calculate(strFormula, FormulaVersion.FV10));

            //??????????
            strFormula = "-1/ -3+ 1 -0 * 4 ";
            dResult = -1 / -3 + 1 - 0 * 4;
            TEST_Item(strFormula, dResult, Formula.Calculate(strFormula, FormulaVersion.FV10));
            strFormula = "+0--1* 2 /-3.5 +-4";
            dResult = +0 - -1 * 2 / -3.5 + -4;
            TEST_Item(strFormula, dResult, Formula.Calculate(strFormula, FormulaVersion.FV10));

            //????????
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