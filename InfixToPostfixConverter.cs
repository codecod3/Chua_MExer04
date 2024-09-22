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

        private static bool IsOperatorAndTrigoFunction(string ch)
        {
            return ch switch
            {
                "sin" => true,
                "cos" => true,
                "tan" => true,
                "asin" => true,
                "atan" => true,
                "acos" => true,
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
            bool isTrigoFunction = false;
            expression = expression.Replace(" ", string.Empty);
            foreach (char ch in expression)
            {
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
                    if (IsOperatorAndTrigoFunction(currentLetter)) isTrigoFunction = true;

                }
                if (IsOpeningDelimiter(ch)) stack.Push(ch.ToString());
                if (IsOperatorAndTrigoFunction(ch.ToString()) || isTrigoFunction)
                {
                    string pal = string.Empty;

                    if (isTrigoFunction)
                    {
                        pal = currentLetter;
                        currentLetter = string.Empty;
                        isTrigoFunction = false;
                    }
                    else pal = ch.ToString();

                    if (stack.Count == 0) stack.Push(pal);
                    
                    else
                    {
                        string peek = stack.Peek();
                        // Algorithm 1.3.A
                        while (IsOperatorAndTrigoFunction(peek))
                        {
                            int precedence = ComparePrecedence(pal, peek);
                            if (precedence == 0)
                            {
                                string op = stack.Pop().ToString();
                                postfix.Enqueue(op);
                            }
                            if (stack.Count == 0 || precedence == 1) break;
                            if (stack.Count == 0) break;
                            peek = stack.Peek();
                        }

                        stack.Push(pal);
                        // if precedence of ch is less than peek
                        // pop operator from the stack, enqueue to postfix
                    }

                }
                else if (IsClosingDelimiter(ch))
                {
                    string peek = stack.Peek();
                    // Algorithm 1.2.A
                    while (IsOperatorAndTrigoFunction(peek))
                    {
                        string op = stack.Pop().ToString();
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
                postfix.Enqueue(stack.Pop().ToString());
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
                switch (op)
                {
                    case "+": return 0;
                    case "sin": return 0;
                    case "asin": return 0;
                    case "cos": return 0;
                    case "acos": return 0;
                    case "tan": return 0;
                    case "atan": return 0;
                    case "-": return 0;
                    case "*": return 1;
                    case "/": return 1;
                    case "^": return 2;

                }
            
            throw new InvalidOperationException("Invalid Operator");
        }

    }
}
