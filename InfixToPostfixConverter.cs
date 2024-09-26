using NUnit.Framework;

namespace Chua_MExer04
{
    public static class InfixToPostfixConverter
    {
        private static bool IsOpeningDelimiter(char ch)
        {
            return ch switch
            {
                '(' => true,
                '[' => true,
                '{' => true,
                _ => false,
            };
        }
        private static bool IsClosingDelimiter(char ch)
        {
            return ch switch
            {
                ')' => true,
                ']' => true,
                '}' => true,
                _ => false,
            };
        }

        private static bool IsTrigoFunction(string str)
        {
            return str switch
            {
                "sin" => true,
                "cos" => true,
                "tan" => true,
                "asin" => true,
                "atan" => true,
                "acos" => true,
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
        public static Queue<string> ToPostFix(string expression)
        {
            var stack = new Stack<string>();
            string currentNumber = string.Empty;
            var postfix = new Queue<string>();
            string currentLetter = string.Empty;
            bool isClosingFirst = false;
            expression = expression.Replace(" ", string.Empty);
            foreach (char ch in expression)
            {
                if (IsClosingDelimiter(ch)) isClosingFirst = true;
                if (IsOperator(ch.ToString())) isClosingFirst = false;// if operator is next to closing delimiter, cancel implied multiplication
                if (IsOpeningDelimiter(ch) && isClosingFirst)
                {
                    stack.Push("*");
                    isClosingFirst = false;
                }

                if (!char.IsDigit(ch) && !string.IsNullOrEmpty(currentNumber) && !ch.Equals('.'))
                {
                    postfix.Enqueue(currentNumber);
                    currentNumber = string.Empty;
                }
                if (ch.Equals('.') || char.IsDigit(ch))
                {
                    currentNumber += ch;
                }
                if (char.IsLetter(ch))
                {
                    currentLetter += ch;
                }
                if (IsOpeningDelimiter(ch)) stack.Push(ch.ToString());
                if (IsOperator(ch.ToString()) || IsTrigoFunction(currentLetter))
                {
                    string stuff = string.Empty;

                    if (IsTrigoFunction(currentLetter))
                    {
                        stuff = currentLetter;
                        currentLetter = string.Empty;
                    }
                    else stuff = ch.ToString();

                    if (stack.Count == 0) stack.Push(stuff);
                    
                    else
                    {
                        string peek = stack.Peek();
                        // Algorithm 1.3.A
                        while (IsOperator(peek) || IsTrigoFunction(peek))
                        {
                            int precedence = ComparePrecedence(stuff, peek);
                            if (precedence == 0)
                            {
                                string op = stack.Pop();
                                postfix.Enqueue(op);
                            }
                            if (stack.Count == 0 || precedence == 1) break;
                            if (stack.Count == 0) break;
                            peek = stack.Peek();
                        }

                        stack.Push(stuff);
                        // if precedence of ch is less than peek
                        // pop operator from the stack, enqueue to postfix
                    }

                }
                else if (IsClosingDelimiter(ch))
                {
                    string peek = stack.Peek();
                    // Algorithm 1.2.A
                    while (IsOperator(peek))
                    {
                        string op = stack.Pop();
                        postfix.Enqueue(op);
                        peek = stack.Peek();
                    }
                    // Algorithm 1.2.B
                    stack.Pop();
                }
            }
            if (!string.IsNullOrEmpty(currentNumber))
            {
                postfix.Enqueue(currentNumber);
                currentNumber = string.Empty;
            }
            // Algorithm 2
            while (stack.Count > 0)
            {
                postfix.Enqueue(stack.Pop());
            }
            return postfix;
        }
        //<summary>
        //    Compares the precedence of the first parameter ch,
        //    to the second parameter, peek.
        //    </summary>
        //    <param name = "ch"> the first operator. </param>
        //    <param name = "peek"> the second operator. </param>
        //    <returns> Return 0 if peek >= ch; return 1 if peek < ch </returns> 
        private static int ComparePrecedence(string ch, string peek)
        {
            if (GetPrecedence(peek) >= GetPrecedence(ch)) return 0;
            else return 1;
        }
        private static bool IsPair(char open, char close)
        {
            if (open == '(' && close == ')') return true;
            if (open == '[' && close == ']') return true;
            if (open == '{' && close == '}') return true;

            return false;
        }
        public static int GetPrecedence(string op)
        {
            if (IsTrigoFunction(op)) return 1;
                switch (op)
                {
                    case "-": return 0;
                    case "+": return 0;
                    /*case "sin": return 1;
                    case "asin": return 1;
                    case "cos": return 1;
                    case "acos": return 1;
                    case "tan": return 1;
                    case "atan": return 1;*/
                    case "*": return 2;
                    case "/": return 2;
                    case "^": return 3;

                }
            
            throw new InvalidOperationException("Invalid Operator");
        }

    }
}
