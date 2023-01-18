using Stove.PCSDK.NET;
using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class StovePCSDKManager : MonoBehaviour
{
    // LoadConfig를 통해 채워지는 설정값
    public string Env;
    public string AppKey;
    public string AppSecret;
    public string GameId;
    public StovePCLogLevel LogLevel;
    public string LogPath;
    
    private StovePCCallback callback;
    private Coroutine runcallbackCoroutine;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    private void OnDestroy()
    {
        if (runcallbackCoroutine != null)
        {
            StopCoroutine(runcallbackCoroutine);
            runcallbackCoroutine = null;
        }

        StovePC.Uninitialize();
    }

    #region Event Handlers
    public void ButtonLoadConfig_Click()
    {
        string configFilePath = Application.streamingAssetsPath + "/Text/StovePCConfig.Unity.txt";

        if (File.Exists(configFilePath))
        {
            string configText = File.ReadAllText(configFilePath);
            StovePCConfig config = JsonUtility.FromJson<StovePCConfig>(configText);

            this.Env = config.Env;
            this.AppKey = config.AppKey;
            this.AppSecret = config.AppSecret;
            this.GameId = config.GameId;
            this.LogLevel = config.LogLevel;
            this.LogPath = config.LogPath;

            WriteLog(configText);
            callback= new StovePCCallback
            {
                OnError = new StovePCErrorDelegate(this.OnError),
                OnInitializationComplete = new StovePCInitializationCompleteDelegate(this.OnInitializationComplete),
                OnToken = new StovePCTokenDelegate(this.OnToken),
                OnUser = new StovePCUserDelegate(this.OnUser)
            };
            StovePC.Initialize(config, callback);
        }
        else
        {
            string msg = String.Format("File not found : {0}", configFilePath);
            WriteLog(msg);
        }
    }

    public void ButtonInitialize_Click()
    {
        StovePCResult sdkResult = StovePCResult.NoError;



        WriteLog("Initialize", sdkResult);
    }

    public void ToggleRunCallback_ValueChanged(bool isOn)
    {
        if (isOn)
        {
            float intervalSeconds = 1f;
            runcallbackCoroutine = StartCoroutine(RunCallback(intervalSeconds));

            WriteLog("RunCallback Start");
        }
        else
        {
            if (runcallbackCoroutine != null)
            {
                StopCoroutine(runcallbackCoroutine);
                runcallbackCoroutine = null;

                WriteLog("RunCallback Stop");
            }
        }
    }

    public void ButtonGetToken_Click()
    {
        StovePCResult sdkResult = StovePCResult.NoError;

        // Todo: 여기에 따라하기 코드를 작성합니다.

        WriteLog("GetToken", sdkResult);
    }

    public void ButtonGetUser_Click()
    {
        StovePCResult sdkResult = StovePCResult.NoError;

        // Todo: 여기에 따라하기 코드를 작성합니다.

        WriteLog("GetUser", sdkResult);
    }

    public void ButtonUninitialize_Click()
    {
        ToggleRunCallback(false);

        StovePCResult sdkResult = StovePC.Uninitialize();

        // Todo: 여기에 따라하기 코드를 작성합니다.

        WriteLog("Uninitialize", sdkResult);
    }
    #endregion


    #region Coroutine
    private IEnumerator RunCallback(float intervalSeconds)
    {
        WaitForSeconds wfs = new WaitForSeconds(intervalSeconds);
        while (true)
        {
            StovePC.RunCallback();
            yield return wfs;
        }
    }
    #endregion


    #region Methods
    private void WriteLog(string functionName, StovePCResult result)
    {
        if (String.IsNullOrEmpty(functionName))
            functionName = "Unknown";

        string msg = String.Format("{0} Success", functionName);
        if (result != StovePCResult.NoError)
        {
            msg = String.Format("{0} Fail : {1}", functionName, result.ToString());
        }

        Debug.Log(msg + Environment.NewLine);

        AppendUILog(msg);
    }
    private void WriteLog(string log)
    {
        Debug.Log(log + Environment.NewLine);

        AppendUILog(log);
    }

    private void AppendUILog(string log)
    {
        return;
        GameObject content = GameObject.Find("ContentLog");
        GameObject textLog = content.transform.GetChild(0).gameObject;
        GameObject copyLog = Instantiate<GameObject>(textLog, content.transform);
        var copyTextComponent = copyLog.GetComponent<Text>();
        copyTextComponent.text = log;
    }

    private void ToggleRunCallback(bool isOn)
    {
        return;
        GameObject toggleRunCallback = GameObject.Find("ToggleRunCallback");
        Toggle toggleComponent = toggleRunCallback.GetComponent<Toggle>();
        toggleComponent.isOn = isOn;
    }
    #endregion


    #region SDK Callback Methods
    private void OnError(StovePCError error)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("OnError");
        sb.AppendFormat(" - error.FunctionType : {0}" + Environment.NewLine, error.FunctionType.ToString());
        sb.AppendFormat(" - error.Result : {0}" + Environment.NewLine, error.Result.ToString());
        sb.AppendFormat(" - error.Message : {0}" + Environment.NewLine, error.Message);
        sb.AppendFormat(" - error.ExternalError : {0}", error.ExternalError.ToString());

        WriteLog(sb.ToString());
    }

    private void OnInitializationComplete()
    {
        StringBuilder sb = new StringBuilder();

        // Todo: 여기에 따라하기 코드를 작성합니다.

        WriteLog(sb.ToString());
    }

    private void OnToken(StovePCToken token)
    {
        StringBuilder sb = new StringBuilder();

        // Todo: 여기에 따라하기 코드를 작성합니다.

        WriteLog(sb.ToString());
    }

    private void OnUser(StovePCUser user)
    {
        StringBuilder sb = new StringBuilder();

        // Todo: 여기에 따라하기 코드를 작성합니다.

        WriteLog(sb.ToString());
    }
    #endregion
}
