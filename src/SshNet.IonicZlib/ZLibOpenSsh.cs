namespace Renci.SshNet.Compression
{
    public sealed class ZlibOpenSsh : Zlib
    {
        public ZlibOpenSsh()
            : base(delayedCompression: true)
        {
        }

        public override string Name
        {
            get { return "zlib@openssh.com"; }
        }
    }
}