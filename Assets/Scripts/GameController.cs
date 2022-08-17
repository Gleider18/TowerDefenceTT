using UnityEngine.SceneManagement;

public static class GameController
{
    public static int money = 15;
    public static int health = 5;
    public static int wave = 0;
    public static bool isSpawnEnded = false;

    public static bool SpendMoney(int amount)
    {
        if (amount <= money)
        {
            money -= amount;
            return true;
        }

        return false;
    }

    public static void GainMoney(int amount)
    {
        money += amount;
    }

    public static void LoseHealth(int amount)
    {
        if (amount >= health)
            RestartGame();
        health -= amount;
    }

    public static void RestartGame()
    {
        health = 5;
        money = 15;
        wave = 0;
        isSpawnEnded = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}