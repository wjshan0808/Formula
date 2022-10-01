using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formula
{
    public abstract class Formula
    {

        //操作符
        private Stack<string> m_stackOperator = new Stack<string>();
        //操作符(数)
        private Stack<string> m_stackOperand = new Stack<string>();


        /// <summary>
        /// 求解字符算术表达式
        /// </summary>
        /// <param name="strFormula">字符算术表达式</param>
        /// <param name="strFormula">版本号</param>
        /// <returns>结果</returns>
        public string Calculate(string strFormula, FormulaVersion enVersion)
        {
            //检查
            if (string.IsNullOrEmpty(strFormula))
            {
                return string.Empty;
            }

            //重置
            {
                m_stackOperand.Clear();
                this.m_stackOperator.Clear();
            }

            Formula oFormula = this;

            switch (enVersion)
            {
                case FormulaVersion.None:
                    {
                    }
                    break;
            }

            if (!Analysis(strFormula, BeginCalculate))
            {
                return EndCalculate();
            }

            //返回结果
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
        /// 结束求解字符算术表达式
        /// </summary>
        /// <returns>结果</returns>
        protected virtual string EndCalculate()
        {
            return string.Empty;
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

    }
}
