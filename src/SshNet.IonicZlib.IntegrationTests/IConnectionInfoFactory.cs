namespace SshNet.IonicZlib.IntegrationTests
{
    using Renci.SshNet;

    public interface IConnectionInfoFactory
    {
        ConnectionInfo Create();
        ConnectionInfo Create(params AuthenticationMethod[] authenticationMethods);
        ConnectionInfo CreateWithProxy();
        ConnectionInfo CreateWithProxy(params AuthenticationMethod[] authenticationMethods);
    }
}
