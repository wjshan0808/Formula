namespace Formula
{

    /// <summary>
    /// ≤‚ ‘
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
    }
}