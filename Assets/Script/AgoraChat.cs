using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agora.Rtc;
public class AgoraChat : MonoBehaviour
{
    public string AppID;
    public string ChannelName;
    private string _token = "007eJxTYHiderLz7kRN7qqTkwsm3km44LIqvDRRZJHj+SXl71Xktt1SYDA0SrJITjE1TkpJNjcxMzFMTEo2sjBPNUtMTU40STNLfvr4ZFpDICMD46EORiAJhiA+G4NTYh4QMjAAADq4ItE=";
    internal IRtcEngine RtcEngine;

    private string[] wordList = new string[]
    {
        "apple", "banana", "cherry", "date", "elderberry",
        "fig", "grape", "honeydew", "kiwi", "lemon",
        "mango", "nectarine", "orange", "papaya", "quince",
        "raspberry", "strawberry", "tangerine", "ugli", "vanilla",
        "watermelon", "xigua", "yam", "zucchini","cake","asc","abc","king","she12","pol1","point2",
        "ppoint3","channnle123","princevoice","voiceking","1234556788","pinky1","kingpol","commanf3"
    };

    public string GetRandomWord()
    {
        int randomIndex = Random.Range(0, wordList.Length);
        return wordList[randomIndex];
    }
    // Start is called before the first frame update
    void Start()
    {
    SetupAudioSDKEngine();
    InitEventHandler();
    ChannelName = GetRandomWord();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetupAudioSDKEngine() {
    // Create an IRtcEngine instance
    RtcEngine = Agora.Rtc.RtcEngine.CreateAgoraRtcEngine();
    RtcEngineContext context = new RtcEngineContext();
    context.appId = AppID;
    context.channelProfile = CHANNEL_PROFILE_TYPE.CHANNEL_PROFILE_LIVE_BROADCASTING;
    context.audioScenario = AUDIO_SCENARIO_TYPE.AUDIO_SCENARIO_DEFAULT;
    // Initialize the instance
    RtcEngine.Initialize(context);
    }
    // Create an instance of the user callback class and set the callback
private void InitEventHandler() {
    UserEventHandler handler = new UserEventHandler(this);
    RtcEngine.InitEventHandler(handler);
}

// Implement your own callback class, which can inherit from the IRtcEngineEventHandler interface class
internal class UserEventHandler : IRtcEngineEventHandler {
    private readonly AgoraChat _audioSample;

    internal UserEventHandler(AgoraChat audioSample) {
        _audioSample = audioSample;
    }

    // Error callback
    public override void OnError(int err, string msg) { }

    // Triggered when the local user successfully joins the channel
    public override void OnJoinChannelSuccess(RtcConnection connection, int elapsed) {
        Debug.Log("OnJoinChannelSuccess Channel" );
    }
    
    // Triggered when a remote user successfully joins the channel
    public override void OnUserJoined(RtcConnection connection, uint uid, int elapsed) {
        Debug.Log("Remote user joined");
    }
    
    // Triggered when a remote user leaves the current channel
    public override void OnUserOffline(RtcConnection connection, uint uid, USER_OFFLINE_REASON_TYPE reason) { }
    }

    public void Join() {
    Debug.Log("Joining "+ChannelName);
    // Enable the audio module
    RtcEngine.EnableAudio();
    // Set channel media options
    ChannelMediaOptions options = new ChannelMediaOptions();
    // Automatically subscribe to all audio streams
    options.autoSubscribeAudio.SetValue(true);
    // Set the channel profile to live broadcasting
    options.channelProfile.SetValue(CHANNEL_PROFILE_TYPE.CHANNEL_PROFILE_COMMUNICATION);
    // Set the user role to broadcaster
    options.clientRoleType.SetValue(CLIENT_ROLE_TYPE.CLIENT_ROLE_BROADCASTER);
    // Join the channel
    RtcEngine.JoinChannel(_token, ChannelName, 0, options);
    }
    public void Leave() {
    Debug.Log("Leaving _channelName");
    // Leave the channel
    RtcEngine.LeaveChannel();
    // Disable the audio module
    RtcEngine.DisableAudio();
}

void OnApplicationQuit() {
    if (RtcEngine != null) {
        Leave();
        // Destroy IRtcEngine
        RtcEngine.Dispose();
        RtcEngine = null;
    }
}


}
