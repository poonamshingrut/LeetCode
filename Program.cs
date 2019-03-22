using System;
using System.Collections.Generic;
using System.Text;

namespace LeetCode
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.ReadKey();
        }

        /// <summary>
        /// 1
        /// </summary>
        public int[] TwoSum(int[] nums, int target)
        {
            Dictionary<int, int> numbers = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; i++)
            {
                int rem = target - nums[i];
                if (numbers.ContainsKey(rem))
                    return new[] { numbers[rem], i };
                else
                    numbers.Add(nums[i], i);
            }
            throw new Exception("No Solution...");
        }
        //------------------------------------------------------------------------
        /// <summary>
        /// 2
        /// </summary>
        public static ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            ListNode resultNode = new ListNode(0);
            ListNode cur1 = l1, cur2 = l2, startNode = resultNode;
            int carry = 0;
            while (!(cur1 == null && cur2 == null))
            {
                var val1 = cur1?.val ?? 0;
                var val2 = cur2?.val ?? 0;
                int sum = carry + val1 + val2;
                carry = sum / 10;
                startNode.next = new ListNode(sum % 10);
                startNode = startNode.next;
                cur1 = cur1?.next;
                cur2 = cur2?.next;
            }
            if (carry > 0)
                startNode.next = new ListNode(carry);
            return resultNode.next;
        }
        //------------------------------------------------------------------------
        /// <summary>
        /// 3
        /// </summary>
        public static int LengthOfLongestSubstring(string s)
        {
            int maxSeq = 0, seqLen = 0;
            Dictionary<char, int> seq = new Dictionary<char, int>();
            for (int i = 0; i < s.Length; i++)
            {
                if (seq.ContainsKey(s[i]))
                {
                    seqLen = Math.Max(seq[s[i]], seqLen);
                    seq[s[i]] = i + 1;
                }
                else
                {
                    seq.Add(s[i], i + 1);
                }
                maxSeq = Math.Max(maxSeq, i - seqLen + 1);

            }
            return maxSeq;
        }
        //------------------------------------------------------------------------
        /// <summary>
        /// 4
        /// </summary>
        public static double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            var m = nums1.Length;
            var n = nums2.Length;
            int[] sorted = new int[m + n];
            int start = 0, i = 0, j = 0;
            bool flag = true;
            while (start < m + n && flag)
            {
                flag = false;
                while (i < nums1.Length && j < nums2.Length && nums1[i] <= nums2[j])
                {
                    sorted[start++] = nums1[i++];
                    flag = true;
                }
                while (i < nums1.Length && j < nums2.Length && nums2[j] < nums1[i])
                {
                    sorted[start++] = nums2[j++];
                    flag = true;
                }
            }
            while (i < nums1.Length)
                sorted[start++] = nums1[i++];
            while (j < nums2.Length)
                sorted[start++] = nums2[j++];

            var middle = (m + n) / 2;
            if ((m + n) % 2 == 0)
                return (sorted[middle - 1] + (double)sorted[middle]) / 2.0;
            else
                return sorted[middle];
        }
        //------------------------------------------------------------------------
        /// <summary>
        /// 5
        /// </summary>
        public static string LongestPalindrome(string s)
        {
            if (s.Length <= 1)
                return s;
            int leftIndex = 0, rightIndex = 0;
            for (int i = 0; i < s.Length; i++)
            {
                int strLenOdd = PalindromeAroundCentre(s, i, i);
                int strLenEven = PalindromeAroundCentre(s, i, i + 1);
                var strLen = Math.Max(strLenOdd, strLenEven);
                if (strLen > rightIndex - leftIndex)
                {
                    leftIndex = i - (strLen - 1) / 2;
                    rightIndex = i + strLen / 2;
                }
            }

            return s.Substring(leftIndex, rightIndex - leftIndex + 1);
        }

        private static int PalindromeAroundCentre(string s, int left, int right)
        {
            while (left >= 0 && right < s.Length && s[left] == s[right])
            {
                left--;
                right++;
            }

            return right - left - 1;
        }
        //------------------------------------------------------------------------
        /// <summary>
        /// 6
        /// </summary>
        public static string ZigzagConvert(string s, int numRows)
        {
            if (s.Length <= 1 || numRows == 1)
                return s;
            else if (numRows == 0)
                return "";
            StringBuilder[] rows = new StringBuilder[numRows];
            bool goingDown = false;
            for (int i = 0; i < numRows; i++)
            {
                rows[i] = new StringBuilder();
            }
            int startIndex = 0;
            foreach (var c in s.ToCharArray())
            {
                rows[startIndex].Append(c);
                if (startIndex == 0 || startIndex == numRows - 1) goingDown = !goingDown;
                startIndex += goingDown ? 1 : -1;
            }

            StringBuilder result = new StringBuilder();
            foreach (var row in rows)
            {
                result.Append(row);
            }

            return result.ToString();
        }
        //------------------------------------------------------------------------
        /// <summary>
        /// 7
        /// </summary>
        public static int Reverse(int x)
        {
            bool negativeNum = x < 0;
            long num = negativeNum ? (-1) * (long)x : x;
            long result = 0;
            while (num > 0)
            {
                result = (result * 10 + num % 10);
                num /= 10;
            }

            result *= negativeNum ? -1 : 1;
            if (result > Int32.MaxValue || result < Int32.MinValue)
                return 0;
            return (int)result;
        }
        //------------------------------------------------------------------------
        /// <summary>
        /// 8
        /// </summary>
        public static int MyAtoi(string str)
        {
            StringBuilder numStr = new StringBuilder();
            bool numStart = false;
            bool signed = false;
            int pos = 0;
            while (pos < str.Length)
            {
                if (char.IsDigit(str[pos]))
                {
                    numStart = true;
                    numStr.Append(str[pos]);
                    pos++;
                }
                else if (!numStart && !signed && str[pos] == ' ')
                    pos++;
                else if (!numStart && !signed && (str[pos] == '+' || str[pos] == '-'))
                {
                    numStr.Append(str[pos]);
                    signed = true;
                    pos++;
                }
                else
                {
                    break;
                }
            }

            try
            {
                return Convert.ToInt32(numStr.ToString());
            }
            catch (OverflowException)
            {
                if (numStr[0] == '-')
                    return Int32.MinValue;
                else
                    return Int32.MaxValue;
            }
            catch
            {
                return 0;
            }

        }
        //------------------------------------------------------------------------
        /// <summary>
        /// 9
        /// </summary>
        public static bool IsPalindrome(int x)
        {
            if (x < 0)
                return false;
            int revert = 0;
            int temp = x;
            while (temp > 0)
            {
                revert = revert * 10 + temp % 10;
                temp /= 10;
            }
            return revert == x;
        }
        //------------------------------------------------------------------------
        /// <summary>
        /// 10
        /// </summary>
        public static bool IsMatch(string s, string p)
        {
            int i = s.Length - 1; int j = p.Length - 1;

            while (i >= 0 && j >= 0)
            {
                if (IsMatch(s[i], p[i]))
                {
                    i--;
                    j--;
                }
                else if (p[j] == '*')
                { }
                else
                {
                    return false;
                }
            }
            return true;
        }

        private static bool IsMatch(char s, char p)
        {
            return (s == p || p == '.');
        }
        //------------------------------------------------------------------------
        /// <summary>
        /// 11
        /// </summary>
        public static int MaxArea(int[] height)
        {
            int maxArea = 0;
            int start = 0, end = height.Length - 1;
            while (end > start)
            {

                var area = Math.Min(height[start], height[end]) * (end - start);
                maxArea = maxArea < area ? area : maxArea;
                if (height[start] < height[end])
                    start++;
                else
                    end--;
            }
            return maxArea;
        }
        //------------------------------------------------------------------------
        /// <summary>
        /// 12  - 1
        /// </summary>
        public static string IntToRoman(int num)
        {
            StringBuilder r = new StringBuilder();
            int[] vals = new[] { 1, 5, 10, 50, 100, 500, 1000 };
            char[] chars = new[] { 'I', 'V', 'X', 'L', 'C', 'D', 'M' };
            for (int i = vals.Length - 1; i >= 0 && num > 0; i--)
            {
                if (num / vals[i] > 0)
                {
                    var repeat = num / vals[i];
                    num -= repeat * vals[i];
                    while (repeat-- > 0)
                        r.Append(chars[i]);
                    i++;
                }
                else if (i - 1 >= 0 && vals[i] / vals[i - 1] == 5 && num >= (vals[i] - vals[i - 1]))
                {
                    r.Append($"{chars[i - 1]}{chars[i]}");
                    num -= (vals[i] - vals[i - 1]);
                }
                else if (i - 2 >= 0 && vals[i] / vals[i - 2] == 10 && num >= (vals[i] - vals[i - 2]))
                {
                    r.Append($"{chars[i - 2]}{chars[i]}");
                    num -= (vals[i] - vals[i - 2]);
                }
            }
            return r.ToString();
        }
        /// 12  - 2
        /// </summary>
        public static string IntToRomanV2(int num)
        {
            StringBuilder r = new StringBuilder();
            int[] vals = new[] { 1, 4, 5, 9, 10, 40, 50, 90, 100, 400, 500, 900, 1000 };
            string[] chars = new[] { "I", "IV", "V", "IX", "X", "XL", "L", "XC", "C", "CD", "D", "CM", "M" };
            for (int i = vals.Length - 1; i >= 0 && num > 0; i--)
            {
                if (num / vals[i] > 0)
                {
                    var repeat = num / vals[i];
                    num -= repeat * vals[i];
                    while (repeat-- > 0)
                        r.Append(chars[i]);
                    i++;
                }
            }
            return r.ToString();
        }
        //------------------------------------------------------------------------
        /// <summary>
        /// 13
        /// </summary>
        public static int RomanToInt(string s)
        {
            Dictionary<char, int> RomanValues = new Dictionary<char, int>()
            {
                {'I', 1},
                {'V', 5},
                {'X', 10},
                {'L', 50},
                {'C', 100},
                {'D', 500},
                {'M', 1000}
            };
            long result = 0;

            for (int i = 0; i < s.Length; i++)
            {
                if (i + 1 < s.Length && RomanValues[s[i]] < RomanValues[s[i + 1]])
                {
                    result += (RomanValues[s[i + 1]] - RomanValues[s[i]]);
                    i++;
                }
                else
                    result += RomanValues[s[i]];
            }

            return result > Int32.MaxValue ? Int32.MaxValue : (int)result;
        }
        //------------------------------------------------------------------------
        /// <summary>
        /// 14
        /// </summary>
        public static string LongestCommonPrefix(string[] strs)
        {

        }
    }
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int x) { val = x; }
    }


}
