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
            Console.WriteLine("There are 2 numbers: " + num1.Value() + " and " + num2.Value());
            Console.WriteLine("Sum equals to " + sum.Value());
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
        public MyFloat(string data)
        {
            // на всякий случай, вдруг ввели с пробелами ...
            data = data.Trim();
            if (data == "")
            {
                this._mantissa = 0;
                this._order = 0;
            }
            else
            {
                var input = data.Split('e');
                this._mantissa = Convert.ToInt64(input[0]);
                this._order = Convert.ToSByte(input[1]);
            }
            
            return;
        }
        
        public string Value() // возвращает значение (строку)
        {
            return (this._mantissa + (this._order != 0 && this._mantissa != 0 ? ("e" + this._order) : ""));
        }

        private static void helpToGetSameOrder(MyFloat a, MyFloat b)
        {
            var maxOrder = (a._order > b._order ? a._order : b._order);
            var minOrder = (a._order < b._order ? a._order : b._order);
            var difference = (maxOrder - minOrder);
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

            return;
        }
        public static bool operator <(MyFloat a1, MyFloat b1)
        {
            var a = new MyFloat(Convert.ToString(a1._mantissa) + "e" + Convert.ToString(a1._order));
            var b = new MyFloat(Convert.ToString(b1._mantissa) + "e" + Convert.ToString(b1._order));
            helpToGetSameOrder(a, b);
            
            if ((a._mantissa < 0 && b._mantissa > 0)||(a._mantissa > 0 && b._mantissa < 0)) // они разных знаков
            {
                return (a._mantissa < b._mantissa);
            }
            else // они одного знака
            {
                if (a._mantissa > 0) // => оба больше нуля
                {
                    if (a._order < b._order)
                        return true;
                    else if (a._order > b._order)
                        return false;
                    else
                        return (a._mantissa < b._mantissa);
                }
                else // => оба меньше нуля
                {
                    if (a._order > b._order)
                        return true;
                    else if (a._order < b._order)
                        return false;
                    else
                        return (a._mantissa > b._mantissa);
                }
            }
        }
        public static bool operator >(MyFloat a1, MyFloat b1)
        {
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
            var a = new MyFloat(Convert.ToString(a1._mantissa) + "e" + Convert.ToString(a1._order));
            var b = new MyFloat(Convert.ToString(b1._mantissa) + "e" + Convert.ToString(b1._order));
            helpToGetSameOrder(a, b);
            return (a._mantissa == b._mantissa && a._order == b._order);
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
            var a = new MyFloat(Convert.ToString(a1._mantissa) + "e" + Convert.ToString(a1._order));
            var b = new MyFloat(Convert.ToString(b1._mantissa) + "e" + Convert.ToString(b1._order));
            helpToGetSameOrder(a, b);
            

            return new MyFloat(Convert.ToString(a._mantissa + b._mantissa) + "e" + Convert.ToString(a._order));
        }

        public static MyFloat operator -(MyFloat a1, MyFloat b1)
        {
            var a = new MyFloat(Convert.ToString(a1._mantissa) + "e" + Convert.ToString(a1._order));
            var b = new MyFloat(Convert.ToString((-1) * b1._mantissa) + "e" + Convert.ToString(b1._order));

            return (a + b);
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

            return ((MyFloat) obj) == this;
        }
    }
}