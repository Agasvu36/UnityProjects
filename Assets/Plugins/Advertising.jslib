mergeInto(LibraryManager.library, {
  
    ShowAdv : function()
    {
        ysdk.adv.showFullscreenAdv({
            callbacks: {
                onClose: function(wasShown) {
                //console.Log("--------showed-------------");// some action after close
                gameInstance.SendMessage("Character", "Resume");
                },
                onError: function(error) {
                   // console.Log("--------error-------------");
                    gameInstance.SendMessage("Character", "Resume");
                // some action on error
                }
            }
            })		
    },
	
	SaveExtern : function(date){
		var dateString = UTF8ToString(date);
		var myobj = JSON.parse(dateString);
		player.setData(myobj);
	},
	
	LoadExtern : function(){
		player.getData().then(_date => {
			const myJSON = JSON.stringify(_date);
			gameInstance.SendMessage('Progress', 'SetPlayerInfo', myJSON);
		});
	},
  
});