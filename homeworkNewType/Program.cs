using System;

namespace homeworkNewType
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var num1 = new MyFloat("130e4");
            var num2 = new MyFloat("13e5");
            var sum = num1 + num2;
            Console.WriteLine("There are 2 numbers: " + num1.Value() + " and " + num2.Value()); // use interpolation 
            Console.WriteLine("Sum equals to " + sum.Value()); // use interpolation
            Console.WriteLine($"({num1.Value()}) < ({num2.Value()}) : {(num1 < num2)}");
            Console.WriteLine($"({num1.Value()}) > ({num2.Value()}) : {(num1 > num2)}");
            Console.WriteLine($"({num1.Value()}) == ({num2.Value()}) : {(num1 == num2)}");
            
            Console.WriteLine($"{num1.Value()} equals to {num2.Value()} : {num1.Equals(num2)}");
        }
    }


    public class MyFloat
    {
        private sbyte _order; // порядок (степень десятки)
        private Int64 _mantissa; // мантисса (число перед e как бы) 
        // use one notation of type or System(CLR) Type or wrappers, so for 'Int64' we have wrapper 'long'
        public MyFloat(string data)
        {
            // constructor responsible for initializing not for preprocessing
            // it is will better if u put reprocessing in separate method
            
            // на всякий случай, вдруг ввели с пробелами ...
            data = data.Trim();
            if (data == "") // use string.Empty 
            {
                this._mantissa = 0; // remove this (everywhere)
                this._order = 0;
            }
            else
            {
                var input = data.Split('e'); // u always use this value but u can put it in constant variable
                this._mantissa = Convert.ToInt64(input[0]);
                this._order = Convert.ToSByte(input[1]);
            }
            
            return; // it is necessary here?
        }
        
        // The name of method should starting with Action naming like GetValue, but even this doesn't display the main idea of this method 
        public string Value() // возвращает значение (строку)
        {
            return (this._mantissa + (this._order != 0 && this._mantissa != 0 ? ("e" + this._order) : ""));
        }

        // private method also naming in PascalCase
        // name doesn't display the main idea of this method 
        private static void helpToGetSameOrder(MyFloat a, MyFloat b)
        {
            var maxOrder = (a._order > b._order ? a._order : b._order); // remove, and please don't use parentheses when not needed 
            var minOrder = (a._order < b._order ? a._order : b._order); // Math.Min(a._order, b._order)
            var difference = (maxOrder - minOrder); // Math.Abs(a._order - b._order); looks better right?
            if (a._order > b._order)
            {
                a._mantissa *= Convert.ToInt64(Math.Pow(10, difference));
                a._order = minOrder;
            }
            else
            {
                b._order = minOrder;
                b._mantissa *= Convert.ToInt64(Math.Pow(10, difference));
            }

            // var minValue = a._order < b._order ? a : b;                          so i think in this keys ternary operator looks better 
            // minValue._order = minOrder;
            // minValue._mantissa *= Convert.ToInt64(Math.Pow(10, difference));
            return; //remove
        }
        public static bool operator <(MyFloat a1, MyFloat b1)
        {
            var a = new MyFloat(Convert.ToString(a1._mantissa) + "e" + Convert.ToString(a1._order)); // u can use a1._mantissa + "e" + a1._order, because when we try add int to string, compile thinks that it is concatenation of strings
            var b = new MyFloat(Convert.ToString(b1._mantissa) + "e" + Convert.ToString(b1._order));
            // and also u can rename a1 and b1 like sourceA{B}, and rename a and b like copyA{B} or tempA{B}
            helpToGetSameOrder(a, b);
            
            if ((a._mantissa < 0 && b._mantissa > 0)||(a._mantissa > 0 && b._mantissa < 0)) // они разных знаков
            {
                return (a._mantissa < b._mantissa);
            }
            else // они одного знака                                             // u can remove else here
            {
                if (a._mantissa > 0) // => оба больше нуля
                {   
                    if (a._order < b._order) // so after call helpToGetSameOrder, orders are the same, why u are compare them?
                        return true;         
                    else if (a._order > b._order)                                // u can remove else here
                        return false;
                    else                                                         // u can remove else here
                        return (a._mantissa < b._mantissa); // that means u can only return (a._mantissa < b._mantissa);
                }
                else // => оба меньше нуля                                        // u can remove else here
                {
                    if (a._order > b._order)
                        return true;
                    else if (a._order < b._order)                                 // u can remove else here
                        return false;
                    else                                                         // u can remove else here
                        return (a._mantissa > b._mantissa);
                }
            }
        } // separate the method with at least 1 line

        public static bool operator >(MyFloat a1, MyFloat b1)
        {
            // but why u are not using something like this
            // return !(a1 == b1 || a1 < b1)
            var a = new MyFloat(Convert.ToString(a1._mantissa) + "e" + Convert.ToString(a1._order)); 
            var b = new MyFloat(Convert.ToString(b1._mantissa) + "e" + Convert.ToString(b1._order));
            
            helpToGetSameOrder(a, b);
            
            if ((a._mantissa < 0 && b._mantissa > 0)||(a._mantissa > 0 && b._mantissa < 0)) // они разных знаков
            {
                return (a._mantissa > b._mantissa);
            }
            else // они одного знака
            {
                if (a._mantissa > 0) // => оба больше нуля
                {
                    if (a._order > b._order)
                        return true;
                    else if (a._order < b._order)
                        return false;
                    else
                        return (a._mantissa > b._mantissa);
                }
                else // => оба меньше нуля
                {
                    if (a._order < b._order)
                        return true;
                    else if (a._order > b._order)
                        return false;
                    else
                        return (a._mantissa < b._mantissa);
                }
            }
        }

        public static bool operator ==(MyFloat a1, MyFloat b1)
        {
            var a = new MyFloat(Convert.ToString(a1._mantissa) + "e" + Convert.ToString(a1._order)); // previous comments
            var b = new MyFloat(Convert.ToString(b1._mantissa) + "e" + Convert.ToString(b1._order)); // u always copy them, maybe u can create method like var a = a1.Copy()
            helpToGetSameOrder(a, b);
            return (a._mantissa == b._mantissa && a._order == b._order); // order the same
        }

        public static bool operator !=(MyFloat a, MyFloat b)
        {
            return !(a == b);
        }

        public static bool operator <=(MyFloat a, MyFloat b)
        {
            return (a < b || a == b);
        }
        
        public static bool operator >=(MyFloat a, MyFloat b)
        {
            return (a > b || a == b);
        }

        public static MyFloat operator +(MyFloat a1 , MyFloat b1)
        {
            var a = new MyFloat(Convert.ToString(a1._mantissa) + "e" + Convert.ToString(a1._order)); // previous comments
            var b = new MyFloat(Convert.ToString(b1._mantissa) + "e" + Convert.ToString(b1._order));
            helpToGetSameOrder(a, b);
            

            return new MyFloat(Convert.ToString(a._mantissa + b._mantissa) + "e" + Convert.ToString(a._order)); // u should normalize number in this case
        }

        public static MyFloat operator -(MyFloat a1, MyFloat b1)
        {
            var a = new MyFloat(Convert.ToString(a1._mantissa) + "e" + Convert.ToString(a1._order));
            var b = new MyFloat(Convert.ToString((-1) * b1._mantissa) + "e" + Convert.ToString(b1._order));

            return (a + b); // u can remove copy, because in + operation, them will be replace by copy 
        }
        
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!(obj is MyFloat))
            {
                return false;
            }

            return ((MyFloat) obj) == this; // so, if you didn't override == you would compare links, remember it 
        }
    }
}