# TicTacToeApi
POST */createGame/{PlayerName}* </br>
Создает игру и записывает *PlayerName* как 1 игрока </br>
</br>
POST */{gameId}/join/{PlayerName}* </br>
Позволяет игроку *PlayerName* присоединится к игре с *gameId* </br>
</br>
POST */createGame/{PlayerName}* </br>
Создает игру и записывает PlayerName как 1 игрока </br>
</br>
POST */gameId* </br>
Возвращает информацию о игре с gameId </br>
</br>
POST *{gameId}/move/{row}/{column}/{PlayerName}* </br>
Игрок с ником PlayerName делает ход в row строке и column колонке </br>
</br>
База данных - **MySQL**
