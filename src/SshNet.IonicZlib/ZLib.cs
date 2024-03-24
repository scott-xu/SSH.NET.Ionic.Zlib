namespace Renci.SshNet.Compression
{
    using System.IO;
    using Ionic.Zlib;

    /// <summary>
    /// Represents "zlib" compression implementation.
    /// </summary>
    public class Zlib : Compressor
    {
        private readonly ZlibStream _compressor;
        private readonly ZlibStream _decompressor;
        private MemoryStream _compressorStream;
        private MemoryStream _decompressorStream;
        private bool _isDisposed;

        public Zlib()
            : this(delayedCompression: false)
        {
        }

        protected Zlib(bool delayedCompression)
            : base(delayedCompression)
        {
            _compressorStream = new MemoryStream();
            _decompressorStream = new MemoryStream();

            _compressor = new ZlibStream(_compressorStream, CompressionMode.Compress);
            _decompressor = new ZlibStream(_decompressorStream, CompressionMode.Decompress);
        }

        /// <summary>
        /// Gets algorithm name.
        /// </summary>
        public override string Name
        {
            get { return "zlib"; }
        }

        protected override byte[] CompressCore(byte[] data, int offset, int length)
        {
            _compressorStream.SetLength(0);

            _compressor.Write(data, offset, length);
            _compressor.Flush();

            return _compressorStream.ToArray();
        }

        protected override byte[] DecompressCore(byte[] data, int offset, int length)
        {
            _decompressorStream.SetLength(0);

            _decompressor.Write(data, offset, length);
            _decompressor.Flush();

            return _decompressorStream.ToArray();
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><see langword="true"/> to release both managed and unmanaged resources; <see langword="false"/> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (_isDisposed)
            {
                return;
            }

            if (disposing)
            {
                var compressorStream = _compressorStream;
                if (compressorStream != null)
                {
                    compressorStream.Dispose();
                    _compressorStream = null;
                }

                var decompressorStream = _decompressorStream;
                if (decompressorStream != null)
                {
                    decompressorStream.Dispose();
                    _decompressorStream = null;
                }

                _isDisposed = true;
            }
        }
    }
}