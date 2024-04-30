[System.Serializable]
public class DataPlayer
{
    public float[] posicion = new float[3];
    public float vida;
    public int municion;

    public DataPlayer (PlayerController player)
    {
        vida = player.vidaPJ;
        municion = player.municion;
        posicion[0] = player.transform.position.x;
        posicion[1] = player.transform.position.y;
        posicion[2] = player.transform.position.z;
    }
}