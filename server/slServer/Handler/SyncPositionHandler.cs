using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using Common;
using slServer.Tools;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace slServer.Handler
{
    class SyncPositionHandler : BaseHandler
    {
        public SyncPositionHandler()
        {
            OpCode = OperationCode.Position;
        }

        public override void OnPerationRequest(OperationRequest operationRequest, SendParameters sendParameters, TheClientPeer peer)
        {
            SLServer.log.Info("收到位置同步协议");
            byte[] datas = DicTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.Position) as byte[];

            MemoryStream stream = new MemoryStream();
            stream.Write(datas, 0, datas.Length);
            stream.Position = 0;
            BinaryFormatter bf = new BinaryFormatter();
            Vector3Data pos = bf.Deserialize(stream) as Vector3Data;

            //SLServer.log.Info("坐标:" + pos.X + "======" + pos.Y + "=====" + pos.Z);
            peer.Pos = pos;

        }
    }
}
