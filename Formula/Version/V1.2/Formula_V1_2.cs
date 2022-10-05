using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formula.Version
{

    /// <summary>
    /// 1.2版本功能:
    ///     1.基本字符算术四则运算
    ///     2.包含科学数的基本字符算术四则运算
    ///     3.包含括号优先级的基本字符算术四则运算
    ///     4.包含有符号括号优先级的基本字符算术四则运算
    /// </summary>
    public sealed class Formula_V1_2 : Formula
    {

        /// <summary>
        /// 结束求解字符算术表达式
        /// </summary>
        /// <returns>结果</returns>
        protected override string EndCalculate()
        {
            //可阶段性操作符 E[0, 2]
            while (0x00 < Operator.Count)
            {
                if (0x02 > Operand.Count)
                {
                    throw new Exception("Invalid Formula PE, Operand count should be more than ONE.");
                }
                string strOperandA = Operand.Pop();
                string strOperandB = Operand.Pop();
                //
                string strOperatorPrior = Operator.Pop();
                //
                Operand.Push(Operate(strOperandB, strOperatorPrior, strOperandA));
            }

            //
            if (0x00 >= Operand.Count)
            {
                throw new Exception("Invalid Formula PE, Operand count should have at least ONE.");
            }

            //最终结果
            return Operand.Pop();
        }

        /// <summary>
        /// 开始求解字符算术表达式
        /// </summary>
        /// <param name="strIndiv">单体操作符</param>
        protected override void BeginCalculate(string strIndiv)
        {
            //非阶段性操作标志条件
            if ((true)
                //阶段操作符
                && (")" != strIndiv)
                //操作符
                && ("+" != strIndiv)
                && ("-" != strIndiv)
                && ("*" != strIndiv)
                && ("/" != strIndiv))
            {
                //操作符
                if ((false)
                    || ("(" == strIndiv)
                    //(正, 负)阶段操作符
                    || ("+(" == strIndiv)
                    || ("-(" == strIndiv))
                {
                    Operator.Push(strIndiv);
                }
                //操作符(数)
                else
                {
                    Operand.Push(strIndiv);
                }

                //下一个单体操作符
                return;
            }

            //先前操作符
            string strOperatorPrior = string.Empty;
            if (0x00 < Operator.Count)
            {
                strOperatorPrior = Operator.Pop();
            }

            //括号阶段
            if (")" == strIndiv)
            {
                //必有先前操作符
                if (string.Empty == strOperatorPrior)
                {
                    throw new Exception("Invalid Formula P0, An Operator should be HERE.");
                }

                //括号阶段内
                if ((true)
                    && ("(" != strOperatorPrior)
                    //(正, 负)阶段操作符
                    && ("+(" != strOperatorPrior)
                    && ("-(" != strOperatorPrior))
                {
                    //可阶段性操作符 E[0, 2]
                    do
                    {
                        if (0x02 > Operand.Count)
                        {
                            throw new Exception("Invalid Formula P1, Operand count should be more than ONE.");
                        }
                        string strOperandA = Operand.Pop();
                        string strOperandB = Operand.Pop();
                        //
                        Operand.Push(Operate(strOperandB, strOperatorPrior, strOperandA));

                        //括号阶段结束
                        strOperatorPrior = Operator.Pop();

                    } while (("(" != strOperatorPrior) && ("+(" != strOperatorPrior) && ("-(" != strOperatorPrior));
                }

                //必为左括号操作符
                if ((true)
                    && ("(" != strOperatorPrior)
                    //(正, 负)阶段操作符
                    && ("+(" != strOperatorPrior)
                    && ("-(" != strOperatorPrior))
                {
                    throw new Exception(string.Format("Invalid Formula P2, Here should be Operator '*(' INSTEAD of '{0}'.", strOperatorPrior));
                }

                //括号阶段
                if ("-(" == strOperatorPrior)
                {
                    if (0x01 > Operand.Count)
                    {
                        throw new Exception("Invalid Formula P3, Operand count should have at least ONE.");
                    }
                    string strOperandA = Operand.Pop();
                    string strOperandB = "0";
                    //伪造运算符求负
                    Operand.Push(Operate(strOperandB, "-", strOperandA));
                }

                //括号阶段外
                if (0x00 < Operator.Count)
                {
                    strOperatorPrior = Operator.Peek();

                    //可阶段性高优先级操作符
                    if ((false)
                        || ("*" == strOperatorPrior)
                        || ("/" == strOperatorPrior))
                    {
                        if (0x02 > Operand.Count)
                        {
                            throw new Exception("Invalid Formula P4, Operand count should be more than ONE.");
                        }
                        string strOperandA = Operand.Pop();
                        string strOperandB = Operand.Pop();
                        //
                        strOperatorPrior = Operator.Pop();
                        //
                        Operand.Push(Operate(strOperandB, strOperatorPrior, strOperandA));
                    }
                }

            }
            //可操作性阶段
            else
            {
                //没有先前操作符
                if (string.Empty == strOperatorPrior)
                {
                    //添加当前操作符
                    Operator.Push(strIndiv);

                    //下一个单体操作符
                    return;
                }

                //先前操作符为阶段操作符
                if ((false)
                    || ("(" == strOperatorPrior)
                    //(正, 负)阶段操作符
                    || ("+(" == strOperatorPrior)
                    || ("-(" == strOperatorPrior))
                {
                    //还原先前操作符
                    Operator.Push(strOperatorPrior);

                    //添加当前操作符
                    Operator.Push(strIndiv);

                    //下一个单体操作符
                    return;
                }

                //先前高优先级操作符
                if ((false)
                    || ("*" == strOperatorPrior)
                    || ("/" == strOperatorPrior))
                {
                    if (0x02 > Operand.Count)
                    {
                        throw new Exception("Invalid Formula P5, Operand count should be more than ONE.");
                    }
                    string strOperandA = Operand.Pop();
                    string strOperandB = Operand.Pop();
                    //
                    Operand.Push(Operate(strOperandB, strOperatorPrior, strOperandA));

                    //当前低优先级操作符
                    if ((false)
                        || ("+" == strIndiv)
                        || ("-" == strIndiv))
                    {
                        //可阶段性操作符
                        if ((true)
                            && (0x00 < Operator.Count)
                            && ("(" != Operator.Peek())
                            //(正, 负)阶段操作符
                            && ("+(" != Operator.Peek())
                            && ("-(" != Operator.Peek()))
                        {
                            if (0x02 > Operand.Count)
                            {
                                throw new Exception("Invalid Formula P6, Operand count should be more than ONE.");
                            }
                            strOperandA = Operand.Pop();
                            strOperandB = Operand.Pop();
                            //更前操作符转为先前操作符
                            strOperatorPrior = Operator.Pop();
                            //
                            Operand.Push(Operate(strOperandB, strOperatorPrior, strOperandA));
                        }
                    }

                    //添加当前操作符...

                }
                //先前低优先级操作符
                else
                {
                    //当前同为低优先级操作符
                    if ((false)
                        || ("+" == strIndiv)
                        || ("-" == strIndiv))
                    {
                        if (0x02 > Operand.Count)
                        {
                            throw new Exception("Invalid Formula P7, Operand count should be more than ONE.");
                        }
                        string strOperandA = Operand.Pop();
                        string strOperandB = Operand.Pop();
                        //
                        Operand.Push(Operate(strOperandB, strOperatorPrior, strOperandA));
                    }
                    else
                    {
                        //还原先前操作符
                        Operator.Push(strOperatorPrior);
                    }

                    //添加当前操作符...

                }

                //添加当前操作符
                Operator.Push(strIndiv);

            }//End_if(")" == strIndiv)_else
            
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

            //字符索引
            int iIndexOfChar = 0x00;
            //单体操作符起始索引
            int iIndexOfStartIndiv = iIndexOfChar;

            //多单体操作符标识
            bool bIsMultiIndiv = false;

            //从左向右解析
            while (strFormula.Length > iIndexOfChar)
            {
                //是否结束单体操作符索引标识
                bool bIsIndexOfEndIndiv = false;

                //先前, 当前字符
                char cCharPrior = ' ', cChar = ' ';
                {
                    //先前字符
                    if (0x00 < iIndexOfChar)
                    {
                        cCharPrior = strFormula[iIndexOfChar - 0x01];
                    }

                    //当前字符
                    cChar = strFormula[iIndexOfChar];
                }

                //(正, 负)操作符
                if ((false)
                    || ('+' == cChar)
                    || ('-' == cChar))
                {
                    //单体操作符结束条件
                    if ((true)
                        //先前字符
                        && (' ' != cCharPrior)
                        //操作符
                        //&& (')' != cCharPrior)
                        && ('(' != cCharPrior)
                        && ('+' != cCharPrior)
                        && ('-' != cCharPrior)
                        && ('*' != cCharPrior)
                        && ('/' != cCharPrior)
                        //科学数
                        && ('e' != cCharPrior)
                        && ('E' != cCharPrior))
                    {
                        bIsIndexOfEndIndiv = true;
                    }
                }
                //单体操作符结束条件
                else if ((false)
                    //操作符
                    || ('*' == cChar)
                    || ('/' == cChar)
                    || ('(' == cChar)
                    || (')' == cChar))
                {
                    bIsIndexOfEndIndiv = true;
                }
                //是多单体操作符
                else
                {
                    bIsMultiIndiv = true;
                }

                //单体操作符结束
                if (bIsIndexOfEndIndiv)
                {
                    //是多单体操作符
                    if (bIsMultiIndiv)
                    {
                        //恢复多单体操作符标识
                        bIsMultiIndiv = false;

                        //取首个单体操作符
                        string strIndiv = strFormula.Substring(iIndexOfStartIndiv, (iIndexOfChar - iIndexOfStartIndiv));
                        if (null != acCallback)
                        {
                            acCallback(strIndiv);
                        }

                        //下一个单体操作符起始索引
                        iIndexOfStartIndiv = iIndexOfChar;

                        //取尾个单体操作符
                        strIndiv = strFormula.Substring(iIndexOfStartIndiv, (iIndexOfChar - iIndexOfStartIndiv + 0x01));
                        if (null != acCallback)
                        {
                            acCallback(strIndiv);
                        }

                        //下一个单体操作符起始索引
                        iIndexOfStartIndiv = iIndexOfChar + 0x01;
                    }
                    else
                    {
                        //取单体操作符
                        string strIndiv = strFormula.Substring(iIndexOfStartIndiv, (iIndexOfChar - iIndexOfStartIndiv + 0x01));
                        if (null != acCallback)
                        {
                            acCallback(strIndiv);
                        }

                        //下一个单体操作符起始索引
                        iIndexOfStartIndiv = iIndexOfChar + 0x01;
                    }

                }

                //下一个字符
                ++iIndexOfChar;

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
            return false;
        }
    }
}
