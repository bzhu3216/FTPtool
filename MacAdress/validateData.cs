using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace MacAdress
{
    public static class validateData
    {


        ///
        public static TResult GetValue<TResult>(this TextBox textBox, Action<TextBox> failed)
                       where TResult : struct
        {
            var type = typeof(TResult);
            var method = type.GetMethod("TryParse", new Type[] { typeof(string), type.MakeByRefType() });
            var parameters = new object[] { textBox.Text, default(TResult) };

            // 若转换失败，执行failed
            if (!(bool)method.Invoke(null, parameters))
            {
                failed(textBox);
                throw new InvalidCastException("输入值格式不正确，请检查输入值。");
            }

            return (TResult)parameters[1];
        }

        ///
        public static TResult GetValue<TResult>(this TextBox textBox, bool isShowError)
           where TResult : struct
        {
            return GetValue<TResult>(textBox, p =>
            {
                if (isShowError)
                {
                    p.Focus();
                    p.SelectAll();
                    MessageBox.Show("输入值格式不正确，请重新输入！",
                        "提示--值类型：" + typeof(TResult).Name,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            });
        }



        /// 
        public static TResult GetValue<TResult>(this TextBox textBox)
             where TResult : struct
        {
            return GetValue<TResult>(textBox, true);
        }
        ///
        ///



    }
    ////////////////////////////////////////////////////









}
