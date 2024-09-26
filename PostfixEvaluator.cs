using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chua_MExer04
{
    public class PostfixEvaluator
    {
        public static float Evaluate(Queue<string> postfix)
        {
            var stack = new Stack<float>();
            float x = 0f;
            float y = 0f;
            float result = 0;
            foreach (string item in postfix)
            {
                //from algo 1.i.
                if (IsNumber(item))
                {
                    float number = Parse(item);
                    stack.Push(number);
                }
                else if (IsOperator(item))
                {
                    x = stack.Pop();
                    y = stack.Pop();
                    result = Calculate(x, y, item);
                    stack.Push(result);
                }
                else if (IsTrigoFunction(item))
                {
                    x = stack.Pop();
                    result = CalculateTrigo(x, item);
                    stack.Push(result);
                }
            }

            result = stack.Pop();
            return result;

        }

        private static float CalculateTrigo(float x, string trigo)
        {

            return trigo switch
            {
                "sin" => (float)Math.Sin(x),
                "cos" => (float)Math.Cos(x),
                "tan" => (float)Math.Tan(x),
                "asin" => (float)Math.Asin(x),
                "acos" => (float)Math.Acos(x),
                "atan" => (float)Math.Atan(x),
                _ => throw new InvalidOperationException("Unknown Trigonometric Function"),
            };


        }

        private static float Calculate(float x, float y, string op)
        {
            return op switch
            {
                "+" => y + x,
                "-" => y - x,
                "*" => y * x,
                "/" => y / x,
                "^" => (float)Math.Pow(y, x),
                _ => throw new InvalidOperationException("Unknown Operator"),

            };
        }

        private static bool IsTrigoFunction(string trigo)
        {
            return trigo switch
            {
                "sin" => true,
                "cos" => true,
                "tan" => true,
                "asin" => true,
                "acos" => true,
                "atan" => true,
                _ => false,
            };

        }
        private static bool IsOperator(string ch)
        {
            return ch switch
            {
                "+" => true,
                "-" => true,
                "*" => true,
                "/" => true,
                "^" => true,
                _ => false,
            };
        }

        private static bool IsNumber(string number)
        {
            float result = 0;
            bool isNUmber = float.TryParse(number, out result);
            return isNUmber;
        }

        private static float Parse(string number)
        {
            float result = 0;
            bool isNumber = float.TryParse(number, out result);
            return result;
        }



    }
}
