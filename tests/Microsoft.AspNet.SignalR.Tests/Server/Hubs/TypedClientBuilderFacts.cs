using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Hubs;
using Moq;
using Xunit;

namespace Microsoft.AspNet.SignalR.Tests.Server.Hubs
{
    public class TypedClientBuilderFacts
    {
        [Fact]
        public void MethodsAreInvokedThrougIClientProxy()
        {
            var mockClientProxy = new Mock<IClientProxy>(MockBehavior.Strict);
            mockClientProxy.Setup(c => c.Invoke(
                    It.Is<string>(methodName => methodName == "send"),
                    It.Is<string>(argument => argument == "fun!")))
                .Returns(Task.FromResult<object>(null));

            var client = TypedClientBuilder<IClientContract>.Build(mockClientProxy.Object);

            client.send("fun!");

            mockClientProxy.Verify();
        }

        public interface IClientContract
        {
            void send(string messages);
        }
    }
}
