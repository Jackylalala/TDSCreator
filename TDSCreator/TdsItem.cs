using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TDSCreator
{
    public class TdsItem : IComparable<TdsItem>
    {
        public int TdsIndex { get; set; } //refer to TDS_ITEM_LIST
        public MemoryStream Data { get; set; }
        public string FileType { get; set; }
        public string FileName { get; set; }
        public string SafeFileName { get; set; }
        public int Page { get; set; }

        public TdsItem(int index, string fileName, string fileType)
        {
            TdsIndex = index;
            FileName = fileName;
            SafeFileName = Path.GetFileName(fileName);
            FileType = fileType;
            Page = 0;
        }

        public TdsItem(int index, string fileName)
        {
            TdsIndex = index;
            FileName = fileName;
            SafeFileName = Path.GetFileName(fileName);
            FileType = Path.GetExtension(fileName).ToLower();
            Page = 0;
        }

        public int CompareTo(TdsItem other)
        {
            if (TdsIndex.CompareTo(other.TdsIndex) != 0)
                return TdsIndex.CompareTo(other.TdsIndex);
            else
                return FileName.CompareTo(other.FileName);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            else
            {
                TdsItem other = (TdsItem)obj;
                return GetHashCode().Equals(other.GetHashCode());
            }
        }

        public override int GetHashCode()
        {
            int result = 37;
            result *= 397;
            result += TdsIndex;
            result *= 397;
            result += FileName.GetHashCode();
            return result;
        }

        public override string ToString()
        {
            return SafeFileName;
        }
    }
}
