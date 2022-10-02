using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formula.Version
{
    /// <summary>
    /// 1.0版本功能:
    ///     1.基本字符算术四则运算
    /// </summary>
    public sealed class Formula_V1_0 : Formula
    {

        /// <summary>
        /// 结束求解字符算术表达式
        /// </summary>
        /// <returns>结果</returns>
        protected override string EndCalculate()
        {
            //单一结果
            if (0x00 >= Operator.Count)
            {
                if (0x01 == Operand.Count)
                {
                    return Operand.Pop();
                }
                throw new Exception(string.Format("Invalid Formula PE, Operand count '{0}' should have ONLY ONE.", Operand.Count));
            }
            //可操作性结果
            else
            {
                if ((true)
                    && (0x01 == Operator.Count)
                    && (Operand.Count == (Operator.Count + 0x01)))
                {
                    string strOperandA = Operand.Pop();
                    string strOperandB = Operand.Pop();
                    //
                    string strOperator = Operator.Pop();
                    //
                    return Operate(strOperandB, strOperator, strOperandA);
                }
                throw new Exception(string.Format("Invalid Formula PE, Operantor count '{0}' should have ONLY ONE, and Operand count '{0}' should have ONLY TWO."
                    , Operator.Count, Operand.Count));
            }
        }

        /// <summary>
        /// 开始求解字符算术表达式
        /// </summary>
        /// <param name="strIndiv">单体操作符</param>
        protected override void BeginCalculate(string strIndiv)
        {
            //单体操作符
            if ((false)
                || ("+" == strIndiv)
                || ("-" == strIndiv)
                || ("*" == strIndiv)
                || ("/" == strIndiv))
            {
                Operator.Push(strIndiv);
            }
            //单体操作符(数)
            else
            {
                Operand.Push(strIndiv);
            }

            //可操作性阶段
            if ((false)
                || (0x00 >= Operator.Count)
                || (Operand.Count != (Operator.Count + 0x01)))
            {
                return;
            }

            //操作符
            string strOperator = string.Empty;
            if (0x00 < Operator.Count)
            {
                strOperator = Operator.Peek();
            }

            //高优先级操作符
            if ((false)
                || ("*" == strOperator)
                || ("/" == strOperator))
            {
                string strOperandA = Operand.Pop();
                string strOperandB = Operand.Pop();
                //
                strOperator = Operator.Pop();
                //
                Operand.Push(Operate(strOperandB, strOperator, strOperandA));
            }
            //低优先级操作符
            else
            {
                //存在操作符优先级裁定条件
                if (0x01 >= Operator.Count)
                {
                    return;
                }

                //操作符
                strOperator = Operator.Pop();
                {
                    //操作符(数)
                    string strOperandA = Operand.Pop();

                    //先前操作符(数)
                    string strOperandB = Operand.Pop();
                    string strOperandC = Operand.Pop();
                    //先前操作符
                    string strOperatorPrior = Operator.Pop();
                    //
                    Operand.Push(Operate(strOperandC, strOperatorPrior, strOperandB).ToString());

                    //还原操作符(数)
                    Operand.Push(strOperandA);
                }

                //还原操作符
                Operator.Push(strOperator);
            }

        }


        /// <summary>
        /// 解析字符算术表达式
        /// </summary>
        /// <param name="strFormula">字符算术表达式</param>
        /// <param name="acCallback">解析回调</param>
        /// <returns>状态</returns>
        protected override bool Analysis(string strFormula, Action<string> acCallback)
        {
            //检查
            if (string.IsNullOrEmpty(strFormula))
            {
                return true;
            }

            //去除空格干扰
            strFormula = strFormula.Replace(" ", "");
            if (string.IsNullOrEmpty(strFormula))
            {
                return true;
            }

            //当前字符索引
            int iIndexOfChar = 0x00;
            //单体操作符起始索引
            int iIndexOfStartIndiv = iIndexOfChar;

            //从左向右解析
            while (strFormula.Length > iIndexOfChar)
            {
                //单体操作符索引结束标识
                bool bIsIndexOfEndIndiv = false;

                //当前字符
                char cChar = strFormula[iIndexOfChar];
                {
                    //(正, 负)操作符
                    if ((false)
                        || ('+' == cChar)
                        || ('-' == cChar))
                    {
                        //有前缀字符
                        if (0x00 < iIndexOfChar)
                        {
                            //前缀字符
                            char cCharPrior = strFormula[iIndexOfChar - 0x01];
                            if ((true)
                                //操作符
                                && ('+' != cCharPrior)
                                && ('-' != cCharPrior)
                                && ('*' != cCharPrior)
                                && ('/' != cCharPrior))
                            {
                                bIsIndexOfEndIndiv = true;
                            }
                        }
                    }

                    //操作符
                    if ((false)
                        || ('*' == cChar)
                        || ('/' == cChar))
                    {
                        bIsIndexOfEndIndiv = true;
                    }
                }

                //单体操作符结束
                if (bIsIndexOfEndIndiv)
                {
                    //取单体操作符(数)
                    if (iIndexOfStartIndiv < iIndexOfChar)
                    {
                        string strIndiv = strFormula.Substring(iIndexOfStartIndiv, (iIndexOfChar - iIndexOfStartIndiv));
                        if (null != acCallback)
                        {
                            acCallback(strIndiv);
                        }
                    }

                    //下一个单体操作符起始索引
                    iIndexOfStartIndiv = iIndexOfChar;

                    //取单体操作符
                    if (iIndexOfStartIndiv == iIndexOfChar)
                    {
                        string strIndiv = strFormula.Substring(iIndexOfStartIndiv, 0x01);
                        if (null != acCallback)
                        {
                            acCallback(strIndiv);
                        }
                    }

                    //下一个字符
                    ++iIndexOfChar;

                    //下一个单体操作符起始索引
                    iIndexOfStartIndiv = iIndexOfChar;
                }
                else
                {
                    //下一个字符
                    ++iIndexOfChar;
                }

                //最后一个单体操作符
                if (strFormula.Length <= iIndexOfChar)
                {
                    //存在条件
                    if (iIndexOfStartIndiv < iIndexOfChar)
                    {
                        string strIndiv = strFormula.Substring(iIndexOfStartIndiv, (iIndexOfChar - iIndexOfStartIndiv));
                        if (null != acCallback)
                        {
                            acCallback(strIndiv);
                        }
                    }
                }

            }//End_While
            
            //
            return true;
        }

    }
}
