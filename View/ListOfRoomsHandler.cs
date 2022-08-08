using UnityEngine;

public class ListOfRoomsHandler
{
    private readonly Menu menu;

    private GameObject[] _roomsList = new GameObject[10];

    public ListOfRoomsHandler(Menu menu)
    {
        this.menu = menu;
    }

    public void RefreshListOfRooms(RoomInfo[] roomsList)
    {
        int countOfRooms = roomsList.Length;

        ClearListOfRooms();

        for (int i = 0; i < countOfRooms; i++)
        {
            string name = roomsList[i].Name;
            int countOfPlayersInRoom = roomsList[i].PlayerCount;

            float yPosition = 525f - (i * 110);
            string caption = name + " " + countOfPlayersInRoom + "/2";

            GameObject room = menu.CreateRoomForList(yPosition, name, caption);

            _roomsList[i] = room;

            if (countOfPlayersInRoom == 2)
                menu.DisableJoinToRoom(room);
        }
    }

    private void ClearListOfRooms()
    {
        for (int i = 0; i < _roomsList.Length; i++)
        {
            if (_roomsList[i] != null)
                menu.DestroyRoom(_roomsList[i]);
        }
    }
}
