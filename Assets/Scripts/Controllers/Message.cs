using UnityEngine;
using System.Collections;
using System.Text;

public class Message{

    public MessageCode code;
    byte[] msg;

    /*PLAYER MESSAGE*/
    public int PlayerId;
    public Vector3 playerPos;
    public bool isShielded;

    /*SPELL MESSAGE*/
    public Vector3 SpellPos, SpellDir;

<<<<<<< 79cac6a72cea1d72570f68ac72c2dee06894da3b
    /*GAMEPREF MESSAGE*/
    int lifeAllowed;
    float timeOfGame;

    public Message()
    {

=======
    public Message()
    {
>>>>>>> adapting to new script message
    }

    public void PlayerMessage(int _playerId, Vector3 _playerPos, bool _isShielded)
    {
        code = MessageCode.MessagePlayer;
        PlayerId = _playerId;
        playerPos = _playerPos;
        isShielded = _isShielded;
        ConvertToByte();
    }

    public void SpellMessage(Vector3 _SpellPos, Vector3 _SpellDir)
    {
        code = MessageCode.MessageSpell;
        SpellPos = _SpellPos;
        SpellDir = _SpellDir;
        ConvertToByte();
    }

    public void GamePrefMessage(int _lifeAllowed, float _timeOfGame)
    {
        code = MessageCode.GamePref;
        lifeAllowed = _lifeAllowed;
        timeOfGame = _timeOfGame;
        ConvertToByte();
    }

    private void ConvertToByte()
    {
        string s = code.ToString() + "#";
        switch (code)
        {
            case MessageCode.MessagePlayer :
                s += PlayerId + "#" + playerPos.x + "#" + playerPos.y + "#" + playerPos.z + "#" + isShielded;
                msg = Encoding.ASCII.GetBytes(s);
                break;
            case MessageCode.MessageSpell:
                s += SpellPos.x + "#" + SpellPos.y + "#" + SpellPos.z + "#" + SpellDir.x + "#" + SpellDir.y + "#" + SpellDir.z;
                break;
            case MessageCode.GamePref:
                s += lifeAllowed + "#" + timeOfGame;
                break;
        }
    }

    public void ConvertFromByte()
    {
        string[] st = ASCIIEncoding.ASCII.GetString(msg).Split('#');
        code = (MessageCode)int.Parse(st[0]);
        switch (code)
        {
            case MessageCode.MessagePlayer:
                PlayerId = int.Parse(st[1]);
                playerPos = new Vector3(float.Parse(st[2]), float.Parse(st[3]), float.Parse(st[4]));
                isShielded = bool.Parse(st[5]);
                break;
            case MessageCode.MessageSpell:
                SpellPos = new Vector3(float.Parse(st[1]), float.Parse(st[2]), float.Parse(st[3]));
                SpellDir = new Vector3(float.Parse(st[4]), float.Parse(st[5]), float.Parse(st[6]));
                break;
            case MessageCode.GamePref:
                lifeAllowed = int.Parse(st[1]);
                timeOfGame = float.Parse(st[2]);
                break;
        }
    }

    public MessageCode GetMessageType()
    {
        return code;
    }

    public byte[] GetByteMessage()
    {
        return msg;
    }

    public void SetByteMessage(byte[] _msg)
    {
        msg = _msg;
    }
}
