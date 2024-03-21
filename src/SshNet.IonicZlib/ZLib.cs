namespace Renci.SshNet.Compression
{
    using System.IO;
    using Ionic.Zlib;

    /// <summary>
    /// Represents "zlib" compression implementation.
    /// </summary>
    public class Zlib : Compressor
    {
        private readonly string _name;

        public Zlib(bool delayedCompression)
            : base(delayedCompression)
        {
            _name = delayedCompression ? "zlib@openssh.com" : "zlib";
        }

        /// <summary>
        /// Gets algorithm name.
        /// </summary>
        public override string Name
        {
            get { return _name; }
        }

        protected override byte[] CompressCore(byte[] data, int offset, int length)
        {
            using var outputStream = new MemoryStream();
            using var zlibStream = new ZlibStream(outputStream, CompressionMode.Compress);

            zlibStream.Write(data, offset, length);

            return outputStream.ToArray();
        }

        protected override byte[] DecompressCore(byte[] data, int offset, int length)
        {
            using var outputStream = new MemoryStream();
            using var zlibStream = new ZlibStream(outputStream, CompressionMode.Decompress);

            zlibStream.Write(data, offset, length);

            return outputStream.ToArray();
        }
    }
}
