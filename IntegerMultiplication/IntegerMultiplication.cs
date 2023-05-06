using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem
{     // salvation 22
    public static class IntegerMultiplication
    {
        #region YOUR CODE IS HERE

        //Your Code is Here:
        //==================
        private static void PrintNum(byte[] X)
        {
            int N = X.Length;
            for (int i = N - 1; i >= 0; i--)
            {
                Console.Write(X[i]);
            }
            Console.WriteLine();
        }
        static public byte[] IntegerMultiply(byte[] X, byte[] Y, int N)
        {
         //   PrintNum(NaiveMultiply(new byte[] { 2, 1 }, new byte[] { 0, 1 }));
             return IntegerMultiply2(X,Y,N);
        }
        static public byte[] IntegerMultiply2(byte[] X, byte[] Y, int N)
        {  
            if(N<=128)
            {
                return NaiveMultiply(X, Y);
            }
            int mid = (N - 1) / 2;
            byte[] b = new byte[N / 2];
            byte[] a = new byte[N / 2];
            byte[] d = new byte[N / 2];
            byte[] c = new byte[N / 2];
            for (int i = 0, j = mid + 1; i <= mid; i++, j++)
            {
                a[i] = X[i];
                c[i] = Y[i];
                b[i] = X[j];
                d[i] = Y[j];
            }
            byte[] AplusB = AddVectors(a, b);
            byte[] CplusD = AddVectors(c, d);
            AplusB = equalize_size(Math.Max(AplusB.Length, CplusD.Length), AplusB);
            CplusD = equalize_size(AplusB.Length, CplusD);
            byte[] bd = IntegerMultiply2(b, d, N / 2);
            byte[] ac = IntegerMultiply2(a, c, N / 2);
            byte[] z = IntegerMultiply2(AplusB, CplusD, AplusB.Length);
            z = SubtractVectors(SubtractVectors(z, bd), ac); ;
            byte[] new_bd = ShiftLeft(bd, N);
            byte[] new_z = ShiftLeft(z, N/2); ;
            byte[] v1 = AddVectors(new_z, new_bd);
            byte[] v2 = AddVectors(v1, ac);

            return v2;
        }
        static public byte[] AddVectors(byte[] a, byte[] b)
        {   
            // The function can remove extra zeroes from the beginning of the number 
            byte carry = 0, sum = 0;
            List<byte> res = new List<byte>();

            for (int i = 0, j = 0; i < a.Length || j<b.Length; i++, j++)
            {
                sum = carry;
                if (i < a.Length) { sum += a[i]; }
                if (j < b.Length) { sum += b[j]; }
                res.Add((byte)(sum % 10));
                carry = (byte)(sum / 10);
            }
            if (carry != 0) res.Add(1);

            while (res.Count > 0 && res[res.Count - 1] == 0)
            {
                res.RemoveAt(res.Count - 1);
            }
            byte[] ans = new byte[res.Count];
            for (int i = 0; i < res.Count; i++)
                ans[i] = res[i];
        
            return ans;
        }
        static public byte[] SubtractVectors(byte[] a, byte[] b)
        {   // The function can remove zeroes in the front of the number 
            List<byte> res = new List<byte>();
            byte borrow = 0;
            for (int i =0, j = 0;  i<a.Length || j<b.Length; i++, j++)
            {
                byte value1 = 0, value2 = 0;
                byte d = 0;
                if (i < a.Length) { value1 = a[i]; }
                if (j < b.Length) { value2 = b[j]; }

                if (value1 < value2 + borrow)
                {
                    d = (byte)(value1 + 10 - value2 - borrow);
                    borrow = 1;
                }
                else
                {
                    d = (byte)(value1 - value2 - borrow);
                    borrow = 0;
                }

                res.Add(d);
            }
            while (res.Count > 0 && res[res.Count - 1] == 0)
            {
                res.RemoveAt(res.Count - 1);
            }
            byte[] ans = new byte[res.Count];
            for (int i = 0; i < res.Count; i++)
            {
                ans[i] = res[i];
            }
            return ans;
        }
        static byte[] ShiftLeft(byte[] x, int places)
        {
            byte[] result = new byte[x.Length + places];
            for(int i = 0; i<places;i++)
            {
                result[i] = 0;
            }
            for (int i = places; i < result.Length; i++)
            {
               result[i] = x[i-places];
            }
            // PrintNum(result);
            return result;
        }
        static public byte[] split_vector(byte[] v, int l, int r)
        {
            byte[] new_vector = new byte[r - l + 1];
            if (v.Length == 0) return new_vector;
            int index = 0;
            for (int i = l; i <= r; i++)
            {
                new_vector[index] = v[i];
                index++;
            }

            return new_vector;
        }
        static public byte[] equalize_size(int newSize, byte[] v)
        {
            int powerOfTwo = 1;
            while (powerOfTwo < newSize) powerOfTwo <<= 1; // Find the nearest power of 2 greater than newSize

            if (powerOfTwo <= v.Length) return v;

            byte[] paddedArray = new byte[powerOfTwo];
            Array.Copy(v, 0, paddedArray, 0, v.Length); // Copy v to the beginning of paddedArray

            return paddedArray;
        }
        static byte[] NaiveMultiply(byte[] arr1, byte[] arr2)
        {   

            //now handling leading zero and remove them 
            int len1 = arr1.Length;
            int len2 = arr2.Length;
            byte[] result = new byte[len1 + len2];
            for (int i = 0; i < len1; i++)
            {
                byte carry = 0;
                for (int j = 0; j < len2; j++)
                {
                    int t = arr1[i] * arr2[j] + carry + result[i + j];
                    result[i + j] = (byte)(t % 10);
                    carry = (byte)(t / 10);
                }
                if (carry > 0)
                {
                    result[i + len2] = carry;
                }
            }
            int lastIndex = result.Length - 1;
            while (lastIndex>=0 && result[lastIndex] == 0)
            {
                lastIndex--;
            }
            byte[] new_res = new byte[lastIndex + 1];
            Array.Copy(result, new_res, lastIndex + 1);

            return new_res;
        }
        #endregion
    }
}
