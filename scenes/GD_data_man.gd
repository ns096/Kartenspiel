extends Node

#this node is necessary because C# can't handle JSON and Godot dictionaries
#this class send the necessary data over to a C# script

var json_all_cards
# Called when the node enters the scene tree for the first time.
func _ready():
	json_all_cards = load_json_file("res://allCards.json")
	#send_data()

func load_json_file(filePath:String):
	var file = File.new()
	file.open(filePath,1)
	var text = file.get_as_text()
	var dict = JSON.parse(text);
	file.close()
	if dict.error != 0:
		print("ErrorString:", dict.error_string)
		return ("Error")
	else:
		#print(dict.result)
		return dict.result

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass
func send_data():
	var data_man = get_parent()
	for card_id in json_all_cards:
		var card = json_all_cards[card_id]
		#print("sending  ", card, " to ", data_man.name)
		data_man.call("addCard",int(card_id), str(card["name"]), int(card["type"]), int(card["element"]))
		