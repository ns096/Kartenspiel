extends Node2D
var all_cards = {
		1:{"type":"type1","name":"Aarg","flavor_text":""},
		2:{"type":"type2","name":"Soraw","flavor_text":""},
		3:{"type":"type3","name":"Inai","flavor_text":""},
		4:{"type":"type4","name":"Vach","flavor_text":""},
		5:{"type":"type5","name":"Olnar","flavor_text":""}
		}

var deck_content = [1,1,2,2,3,3,4,4,5,5]

var current_deck = []

func draw_from_top(amount):
	return current_deck.pop_front()
	
func load_deck(deck_content):
	current_deck.clear()
	for card in deck_content:
		current_deck.append(card)

func get_card_by_id(id):
	return all_cards[id]

func shuffle_deck():
	var shuffled_deck = []
	var indexList = range(current_deck.size())
	for i in range(current_deck.size()):
	    randomize()
	    var x = randi()%indexList.size()
	    shuffled_deck.append(current_deck[x])
	    indexList.remove(x)
	    current_deck.remove(x)
	current_deck.clear()
	for card in shuffled_deck:
		current_deck.append(card)

func _ready():
	load_deck(deck_content)
	shuffle_deck()
	print(current_deck)
	print(get_card_by_id(2))