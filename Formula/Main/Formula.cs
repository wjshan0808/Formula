using Formula;
using Formula.Version;
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
            Formula oFormula = Calculate(enVersion);

            //解析并求解字符算术表达式
            if (null != oFormula)
            {
                return oFormula.Calculate(strFormula);
            }

            //返回结果
            return string.Empty;
        }

        /// <summary>
        /// 获取字符算术表达式版本实例
        /// </summary>
        /// <param name="enVersion">版本</param>
        /// <returns>版本实例</returns>
        public static Formula Calculate(FormulaVersion enVersion)
        {
            //版本实例
            Formula oFormula = null;
            {
                switch (enVersion)
                {
                    //V1.2
                    case FormulaVersion.FV12:
                        oFormula = new Formula_V1_2();
                        break;
                    //V1.1
                    case FormulaVersion.FV11:
                        oFormula = new Formula_V1_1();
                        break;
                    //V1.0
                    case FormulaVersion.FV10:
                        oFormula = new Formula_V1_0();
                        break;
                    //
                    case FormulaVersion.None:
                        break;
                }
            }

            //
            return oFormula;
        }


        /// <summary>
        /// 求解字符算术表达式
        /// </summary>
        /// <param name="strFormula">字符算术表达式</param>
        /// <returns>结果</returns>
        public string Calculate(string strFormula)
        {
            //检查
            if (string.IsNullOrEmpty(strFormula))
            {
                return string.Empty;
            }

            //解析并求解字符算术表达式
            {
                this.BeginAnalysis();
                if (!this.Analysis(strFormula, this.BeginCalculate))
                {
                    return this.EndCalculate();
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


        /// <summary>
        /// 计算字符算术表达式
        /// </summary>
        /// <param name="strOperandA">操作数A</param>
        /// <param name="strOperator">操作符</param>
        /// <param name="strOperandB">操作数B</param>
        /// <returns>结果</returns>
        protected virtual string Operate(string strOperandA, string strOperator, string strOperandB)
        {
            //检查
            if ((false)
                || (string.IsNullOrEmpty(strOperandA))
                || (string.IsNullOrEmpty(strOperator))
                || (string.IsNullOrEmpty(strOperandB)))
            {
                throw new ArgumentNullException("Parameters is NULL.");
            }

            //浮点类型
            if ((false)
                || (-1 != strOperandA.IndexOfAny(new char[] { '.', 'e', 'E' }))
                || (-1 != strOperandB.IndexOfAny(new char[] { '.', 'e', 'E' })))
            {
                //A
                double dOperandA = 0.0;
                if (!double.TryParse(strOperandA, out dOperandA))
                {
                    throw new FormatException(string.Format("OperandA '{0}' invalid FORMAT.", strOperandA));
                }
                //B
                double dOperandB = 0.0;
                if (!double.TryParse(strOperandB, out dOperandB))
                {
                    throw new FormatException(string.Format("OperandB '{0}' invalid FORMAT.", strOperandB));
                }

                return OperateDecimal(dOperandA, strOperator, dOperandB).ToString();
            }
            //整数类型
            else
            {
                //A
                long lOperandA = 0x00;
                if (!long.TryParse(strOperandA, out lOperandA))
                {
                    throw new FormatException(string.Format("OperandA '{0}' invalid FORMAT.", strOperandA));
                }
                //B
                long lOperandB = 0x00;
                if (!long.TryParse(strOperandB, out lOperandB))
                {
                    throw new FormatException(string.Format("OperandB '{0}' invalid FORMAT.", strOperandB));
                }

                return OperateInteger(lOperandA, strOperator, lOperandB).ToString();
            }
        }

        /// <summary>
        /// 计算浮点字符算术表达式
        /// </summary>
        /// <param name="strOperandA">操作数A</param>
        /// <param name="strOperator">操作符</param>
        /// <param name="strOperandB">操作数B</param>
        /// <returns>结果</returns>
        private double OperateDecimal(double dOperandA, string strOperator, double dOperandB)
        {
            //Operate
            {
                if (false)
                {

                }
                else if ("+" == strOperator)
                {
                    return (dOperandA + dOperandB);
                }
                else if ("-" == strOperator)
                {
                    return (dOperandA - dOperandB);
                }
                else if ("*" == strOperator)
                {
                    return (dOperandA * dOperandB);
                }
                else if ("/" == strOperator)
                {
                    if (0.0 == dOperandB)
                    {
                        throw new DivideByZeroException("Parameter OperandB is ZERO.");
                    }
                    return (dOperandA / dOperandB);
                }
                else
                {
                }
            }

            //无效的操作符
            throw new NotSupportedException(string.Format("Operator '{0}' is NOT supported.", strOperator));
        }

        /// <summary>
        /// 计算整型字符算术表达式
        /// </summary>
        /// <param name="strOperandA">操作数A</param>
        /// <param name="strOperator">操作符</param>
        /// <param name="strOperandB">操作数B</param>
        /// <returns>结果</returns>
        private long OperateInteger(long lOperandA, string strOperator, long lOperandB)
        {
            //Operate
            {
                if (false)
                {

                }
                else if ("+" == strOperator)
                {
                    return (lOperandA + lOperandB);
                }
                else if ("-" == strOperator)
                {
                    return (lOperandA - lOperandB);
                }
                else if ("*" == strOperator)
                {
                    return (lOperandA * lOperandB);
                }
                else if ("/" == strOperator)
                {
                    if (0x00 == lOperandB)
                    {
                        throw new DivideByZeroException("Parameter OperandB is ZERO.");
                    }
                    return (lOperandA / lOperandB);
                }
                else
                {
                }
            }

            //无效的操作符
            throw new NotSupportedException(string.Format("Operator '{0}' is NOT supported.", strOperator));
        }


    }
}
