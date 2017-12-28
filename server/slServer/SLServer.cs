using Common;
using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using log4net.Config;
using Photon.SocketServer;
using slServer.Handler;
using slServer.Threads;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace slServer
{
    public class SLServer : ApplicationBase
    {
        public static SLServer instance {
            get;
            private set;
        }

        public static readonly ILogger log = LogManager.GetCurrentClassLogger();
        private SyncPosThread syncThread = new SyncPosThread();
        public Dictionary<OperationCode, BaseHandler> DicHandler = new Dictionary<OperationCode, BaseHandler>();
        public List<TheClientPeer> PeerList = new List<TheClientPeer>();
        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            log.Info("a client connecting");
            TheClientPeer peer = new TheClientPeer(initRequest);
            PeerList.Add(peer);
            return peer;
        }

        protected override void Setup()
        {
            instance = this;
            log4net.GlobalContext.Properties["Photon:ApplicationLogPath"] = Path.Combine(Path.Combine(this.ApplicationRootPath, "bin_Win64"), "log");
            FileInfo configFileInfo = new FileInfo(Path.Combine(this.BinaryPath, "log4net.config"));
            if (configFileInfo.Exists)
            {
                LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance);
                XmlConfigurator.ConfigureAndWatch(configFileInfo);
            }
            InitDic();
            log.Info("Step Completed!");
            syncThread.Run();
        }
        private void InitDic()
        {
            LoginHandler login = new LoginHandler();
            DicHandler.Add(OperationCode.Login, login);
            DefaultHandler def = new DefaultHandler();
            DicHandler.Add(OperationCode.Default,def);
            RegisterHandler reg = new RegisterHandler();
            DicHandler.Add(OperationCode.Register, reg);
            SyncPositionHandler pos = new SyncPositionHandler();
            DicHandler.Add(OperationCode.Position, pos);
            SyncPlayerHandler player = new SyncPlayerHandler();
            DicHandler.Add(OperationCode.Player, player);
        }
        protected override void TearDown()
        {
            syncThread.Stop(); 
        }
    }
}
