using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace LR3RSA
{
  class Program
  {
    static string ishod;
    static int num;
    static char[] alphabet = new char[] { ' ','А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И', 'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С', 'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ъ', 'Ы', 'Ь', 'Э', 'Ю', 'Я','!','?','.',',' };
    static void Main(string[] args)
    {
      bool vxod = false;
      while (vxod == false)
      {
        
      Console.WriteLine("1 - ассиметричное шифрование (RSA) (шифрование)");
      Console.WriteLine("2 - ассиметричное шифрование (RSA) (дешифрование)");
      num = Convert.ToInt32(Console.ReadLine());

        switch (num)
        {
          case 1:
            RSAEncryption();
            break;
          case 2:
            RSADecryption();
            break;
        }

      }  
    }
    static void RSAEncryption()
    {
      Console.WriteLine("Введите 2 простых числа");
      Console.Write("p = ");
      long p= Convert.ToInt64(Console.ReadLine());
      Console.Write("q = ");
      long q = Convert.ToInt64(Console.ReadLine());

      if (IsTheNumberSimple(p) && IsTheNumberSimple(q))
      {
        long mod = p * q;
        //Console.WriteLine("mod = "+mod);
        long fi = (p - 1) * (q - 1);
       // Console.WriteLine("fi = " + fi);
        long e_ = Calculate_e(fi);
        //Console.WriteLine("e_ = " + e_);
        long d_ = Calculate_d(e_, fi);
        //Console.WriteLine("d_ = " + d_);
        Console.WriteLine("Открытый ключ (e="+ e_ +", mod="+ mod+")");
        Console.Write("Введите слово для шифрования: ");
        ishod = Console.ReadLine();
        RSA_Encode(e_, mod);
        Console.WriteLine(" ");
        Console.WriteLine("Закрытый ключ (d=" + d_ + ", mod=" + mod + ")");

      }
      else
      {
        Console.WriteLine("Эти числа не натуральные");

        RSAEncryption();
      }
      

    }
    static void RSADecryption()
    {
      Console.WriteLine("Введите секретный ключ: ");
      Console.Write("d = ");
      int d_ = Convert.ToInt32(Console.ReadLine());
      Console.Write("mod = ");
      int mod = Convert.ToInt32(Console.ReadLine());
      RSA_Decode(d_, mod);
    }
    static bool IsTheNumberSimple(long n)
    {
      if (n < 2)
        return false;

      if (n == 2)
        return true;

      for (long i = 2; i < n; i++)
        if (n % i == 0)
          return false;
      return true;
    }
    static long Calculate_d(long e, long fi)
    {
      long d = 0;

      while (true)
      {
        if (((d * e) % fi == 1) && (d!=e))
          break;
        else
          d++;
      }

      return d;
    }
    static long Calculate_e(long fi_)
    {
      long e = 2;

      for (long i = 2; i <= fi_; i++)
        if ((fi_ % i == 0) && (e % i == 0)) //если имеют общие делители
        {
          e++;
        }

      return e;
    }
    static void RSA_Encode(long e,long mod)
    {
     
       
      for (int i = 0; i < ishod.Length; i++)
      {
        int x = Array.IndexOf(alphabet, Convert.ToChar(ishod[i].ToString().ToUpper()));
        double z = (Math.Pow(x, e)) % mod;
        Console.Write(z);
        if (i!=ishod.Length-1)
        {
          Console.Write(",");
        }
       
      }
    }
    static void RSA_Decode(long d,long mod)
    {
      Console.WriteLine("Введите зашифрованные символы: ");
      var numbers = Console.ReadLine().Split(',').Select(x => int.Parse(x)).ToList();

      Console.WriteLine("Зашифрованное слово: ");
      foreach (int item in numbers)
      {
        BigInteger x = item;
        x = (BigInteger.Pow(x,(int)d)) % mod;
        var h = (int)(x % int.MaxValue);
        Console.Write(alphabet[h].ToString().ToLower());
      }
      Console.WriteLine(" ");
    }
  }
}

