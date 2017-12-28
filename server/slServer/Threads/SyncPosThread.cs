using Common;
using Photon.SocketServer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace slServer.Threads
{

    public class SyncPosThread
    {
        private Thread thread;

        public void Run()
        {
            thread = new Thread(SyncPos);
            thread.IsBackground = true;
            thread.Start();
        }
        public void Stop()
        {
            thread.Abort();
        }

        private void SyncPos()
        {
            while (true)
            {
                Thread.Sleep(100);
                SendPosition();
            }
            
        }

        private void SendPosition()
        {
            List<PlayerData> playerList = new List<PlayerData>();
            foreach (TheClientPeer item in SLServer.instance.PeerList)
            {
                if (string.IsNullOrEmpty(item.Username) == false)
                {
                    PlayerData playerData = new PlayerData();
                    playerData.PosData = item.Pos;
                    playerData.name = item.Username;
                    playerList.Add(playerData);
                }
            }

            MemoryStream stream = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(stream, playerList);
            byte[] datas = stream.ToArray();
            stream.Close();

            Dictionary<byte, object> data = new Dictionary<byte, object>();
            data.Add((byte)ParameterCode.PlayerList, datas);
            foreach (TheClientPeer item in SLServer.instance.PeerList)
            {
                if (string.IsNullOrEmpty(item.Username) == false)
                {
                    EventData evn = new EventData((byte)EventCode.SyncPosition);
                    evn.SetParameters(data);
                    item.SendEvent(evn, new SendParameters());
                }
            }
        }
    }
}
