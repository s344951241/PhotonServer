using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using Common;
using System.IO;
using System.Xml.Serialization;

namespace slServer.Handler
{
    public class SyncPlayerHandler : BaseHandler
    {
        public SyncPlayerHandler()
        {
            OpCode = OperationCode.Player;
        }
        public override void OnPerationRequest(OperationRequest operationRequest, SendParameters sendParameters, TheClientPeer peer)
        {
            List<string> userNameList = new List<string>();
            foreach (var item in SLServer.instance.PeerList)
            {
                if (!string.IsNullOrEmpty(item.Username)&&(item!=peer))
                {
                    userNameList.Add(item.Username);
                }
            }

            StringWriter sw = new StringWriter();
            XmlSerializer serializer = new XmlSerializer(typeof(List<string>));
            serializer.Serialize(sw, userNameList);
            sw.Close();
            string usernameListString = sw.ToString();

            Dictionary<byte, object> data = new Dictionary<byte, object>();
            data.Add((byte)ParameterCode.UserNameList, usernameListString);
            OperationResponse response = new OperationResponse(operationRequest.OperationCode);
            response.Parameters = data;
            peer.SendOperationResponse(response, sendParameters);

            foreach (TheClientPeer temPeer in SLServer.instance.PeerList)
            {
                if (string.IsNullOrEmpty(temPeer.Username) == false && temPeer != peer)
                {
                    EventData env = new EventData((byte)EventCode.NewPlayer);
                    Dictionary<byte, object> data2 = new Dictionary<byte, object>();
                    data2.Add((byte)ParameterCode.Username, peer.Username);
                    env.SetParameters(data2);
                    temPeer.SendEvent(env, sendParameters);
                }
            }
        }
    }
}
