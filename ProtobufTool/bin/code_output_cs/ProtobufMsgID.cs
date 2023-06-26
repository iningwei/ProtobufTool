public enum ProtobufMsgID
{

    //---------->上行
    [ProtobufMsgIDDes("C2SRoomLogin",100)]
    C2SRoomLogin,
    
    //玩家分数上传
    [ProtobufMsgIDDes("C2SUserScore",2000)]
    C2SUserScore,
    
    //心跳
    [ProtobufMsgIDDes("C2SPing",10000)]
    C2SPing,
    

    //---------->下行
    // 100指令错误,101成功，102无权限，103任务开启失败
    [ProtobufMsgIDDes("S2CRoomLogin",101)]
    S2CRoomLogin,
    
    //直播间评论数据推送
    [ProtobufMsgIDDes("S2CLiveComment",1000)]
    S2CLiveComment,
    
    //直播间礼物数据推送
    [ProtobufMsgIDDes("S2CLiveGift",1001)]
    S2CLiveGift,
    
    //直播间点赞数据推送
    [ProtobufMsgIDDes("S2CLiveLike",1002)]
    S2CLiveLike,
    
    //玩家排名推送
    [ProtobufMsgIDDes("S2CUserScoreSeason",2001)]
    S2CUserScoreSeason,
    
    //服务端下行时把客户端的index传过来
    [ProtobufMsgIDDes("S2CPing",10001)]
    S2CPing,
    
}
