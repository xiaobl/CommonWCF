using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFService
{
   /// <summary>
    /// 自定义读取流（只读）
    /// </summary>
    internal class CusStreamReader : Stream
    {
        long _endPosition;//结束位置
        Stream innerStream;
        /// <summary>
        /// 参数为当前流的断点
        /// </summary>
        public event Action<long> Reading;

        /// <summary>
        /// 直接使用原始流。
        /// </summary>
        /// <param name="stream">原始流</param>
        public CusStreamReader(Stream stream)
        {
            this.innerStream = stream;
            _endPosition = stream.Length;
        }
        /// <summary>
        /// 使用流当前位置，指定长度初始化自定义流
        /// </summary>
        /// <param name="stream">原始流</param>
        /// <param name="count">使用长度</param>
        public CusStreamReader(Stream stream, long count)
        {
            this.innerStream = stream;
            _endPosition = stream.Position + count;
            if (_endPosition > stream.Length)
                _endPosition = stream.Length;
        }
        /// <summary>
        /// 指定初始位置、长度初始化自定义流
        /// </summary>
        /// <param name="stream">原始流</param>
        /// <param name="offset">初始位置</param>
        /// <param name="count">使用长度</param>
        public CusStreamReader(Stream stream, long offset, long count)
        {
            stream.Position = offset > stream.Length ? stream.Length : offset;
            this.innerStream = stream;
            _endPosition = offset + count;
            if (_endPosition > stream.Length)
                _endPosition = stream.Length;
        }
        /// <summary>
        /// 从自定义流读取指定长度到array，但是不超过初始化时设定的长度。
        /// </summary>
        /// <returns>读取的字节数</returns>
        public override int Read(byte[] array, int offset, int count)
        {
            int readcount = 0;
            if (Position + count > this._endPosition)
                readcount = innerStream.Read(array, offset, (int)(this._endPosition - Position));
            else
                readcount = innerStream.Read(array, offset, count);
            if (Reading != null)
                Reading(Position);
            return readcount;
        }
        /// <summary>
        /// 从自定义流读取一个字节，但是不超过初始化时设定的长度。
        /// </summary>
        /// <returns>读取的字节，未找到则返回-1</returns>
        public override int ReadByte()
        {
            if (Position >= this._endPosition)
                return -1;
            else
                return base.ReadByte();
        }

        public override bool CanRead
        {
            get { return innerStream.CanRead; }
        }

        public override bool CanSeek
        {
            get { return false; }
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void Flush()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 自定义流剩余长度。
        /// </summary>
        public override long Length
        {
            get { return _endPosition - innerStream.Position; }
        }

        /// <summary>
        /// 自定义流位置，返回原始流的位置
        /// </summary>
        public override long Position
        {
            get
            {
                return innerStream.Position;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }
    }
}
