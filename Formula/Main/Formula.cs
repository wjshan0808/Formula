using Formula;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formula
{
    /// <summary>
    /// 字符算术表达式
    /// </summary>
    public abstract class Formula
    {
        /// <summary>
        /// 操作符
        /// </summary>
        protected Stack<string> Operator { get { return m_stackOperator; } }
        //操作符
        private Stack<string> m_stackOperator = new Stack<string>();

        /// <summary>
        /// 操作符(数)
        /// </summary>
        protected Stack<string> Operand { get { return m_stackOperand; } }
        //操作符(数)
        private Stack<string> m_stackOperand = new Stack<string>();


        /// <summary>
        /// 求解字符算术表达式
        /// </summary>
        /// <param name="strFormula">字符算术表达式</param>
        /// <param name="enVersion">版本</param>
        /// <returns>结果</returns>
        public static string Calculate(string strFormula, FormulaVersion enVersion)
        {
            //检查
            if (string.IsNullOrEmpty(strFormula))
            {
                return string.Empty;
            }

            //版本实例
            Formula oFormula = null;
            {
                switch (enVersion)
                {
                    //
                    case FormulaVersion.None:
                        break;
                }
            }

            //解析并求解字符算术表达式
            if (null != oFormula)
            {
                oFormula.BeginAnalysis();
                if (!oFormula.Analysis(strFormula, oFormula.BeginCalculate))
                {
                    return oFormula.EndCalculate();
                }
            }

            //返回结果
            return string.Empty;
        }


        /// <summary>
        /// 结束求解字符算术表达式
        /// </summary>
        /// <returns>结果</returns>
        protected virtual string EndCalculate()
        {
            return string.Empty;
        }

        /// <summary>
        /// 开始求解字符算术表达式
        /// </summary>
        /// <param name="strIndiv">单体操作符</param>
        protected virtual void BeginCalculate(string strIndiv)
        {
            Console.WriteLine(strIndiv);
        }

        /// <summary>
        /// 解析字符算术表达式
        /// </summary>
        /// <param name="strFormula">字符算术表达式</param>
        /// <param name="acCallback">解析回调</param>
        /// <returns>状态</returns>
        protected virtual bool Analysis(string strFormula, Action<string> acCallback)
        {
            Console.WriteLine(strFormula);

            return true;
        }

        /// <summary>
        /// 开始解析字符算术表达式
        /// </summary>
        private void BeginAnalysis()
        {
            this.m_stackOperand.Clear();
            this.m_stackOperator.Clear();
        }

    }
}
