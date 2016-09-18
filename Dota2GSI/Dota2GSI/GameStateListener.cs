using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;

namespace Dota2GSI
{
    public delegate void NewGameStateHandler(GameState gamestate);

    public class GameStateListener : IDisposable
    {
        private bool isRunning = false;
        private int connection_port;
        private HttpListener net_Listener;
        private AutoResetEvent waitForConnection = new AutoResetEvent(false);
        private GameState currentGameState;

        public GameState CurrentGameState
        {
            get
            {
                return currentGameState;
            }
            private set
            {
                currentGameState = value;
                RaiseOnNewGameState();
            }
        }

        /// <summary>
        /// Gets the port that is being listened
        /// </summary>
        public int Port { get { return connection_port; } }

        /// <summary>
        /// Returns whether or not the listener is running
        /// </summary>
        public bool Running { get { return isRunning; } }

        /// <summary>
        ///  Event for handing a newly received game state
        /// </summary>
        public event NewGameStateHandler NewGameState = delegate { };

        /// <summary>
        /// A GameStateListener that listens for connections on http://localhost:port/
        /// </summary>
        /// <param name="Port"></param>
        public GameStateListener(int Port)
        {
            connection_port = Port;
            net_Listener = new HttpListener();
            net_Listener.Prefixes.Add("http://localhost:" + Port + "/");
        }

        /// <summary>
        /// A GameStateListener that listens for connections to the specified URI
        /// </summary>
        /// <param name="URI">The URI to listen to</param>
        public GameStateListener(string URI)
        {
            if (!URI.EndsWith("/"))
                URI += "/";

            Regex URIPattern = new Regex("^https?:\\/\\/.+:([0-9]*)\\/$", RegexOptions.IgnoreCase);
            Match PortMatch = URIPattern.Match(URI);

            if (!PortMatch.Success)
                throw new ArgumentException("Not a valid URI: " + URI);

            connection_port = Convert.ToInt32(PortMatch.Groups[1].Value);

            net_Listener = new HttpListener();
            net_Listener.Prefixes.Add(URI);
        }

        /// <summary>
        /// Starts listening for GameState requests
        /// </summary>
        public bool Start()
        {
            if (!isRunning)
            {
                Thread ListenerThread = new Thread(new ThreadStart(Run));
                try
                {
                    net_Listener.Start();
                }
                catch (HttpListenerException)
                {
                    return false;
                }
                isRunning = true;

                // Set this to true, so when the program wants to terminate,
                // this thread won't stop the program from exiting.
                ListenerThread.IsBackground = true;

                ListenerThread.Start();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Stops listening for GameState requests
        /// </summary>
        public void Stop()
        {
            isRunning = false;
        }

        private void Run()
        {
            while (isRunning)
            {
                net_Listener.BeginGetContext(ReceiveGameState, net_Listener);
                waitForConnection.WaitOne();
                waitForConnection.Reset();
            }
            net_Listener.Stop();
        }

        private void ReceiveGameState(IAsyncResult result)
        {
            try
            {
                HttpListenerContext context = net_Listener.EndGetContext(result);
                HttpListenerRequest request = context.Request;
                string JSON;

                waitForConnection.Set();

                using (Stream inputStream = request.InputStream)
                {
                    using (StreamReader sr = new StreamReader(inputStream))
                        JSON = sr.ReadToEnd();
                }
                using (HttpListenerResponse response = context.Response)
                {
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.StatusDescription = "OK";
                    response.Close();
                }
                CurrentGameState = new GameState(JSON);
            }
            catch (ObjectDisposedException)
            {
                // Intentionally left blank, when the Listener is closed.
            }
        }

        private void RaiseOnNewGameState()
        {
            foreach (Delegate d in NewGameState.GetInvocationList())
            {
                if (d.Target is ISynchronizeInvoke)
                    (d.Target as ISynchronizeInvoke).BeginInvoke(d, new object[] { CurrentGameState });
                else
                    d.DynamicInvoke(CurrentGameState);
            }
        }

        public void Dispose()
        {
            this.Stop();
            this.waitForConnection.Dispose();
            this.net_Listener.Close();
        }
    }
}
