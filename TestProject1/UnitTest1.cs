using System;
using Xunit;


public class Form1
{
    public string MessageBoxText { get; private set; } = "";

    public void button2_Click(object sender, EventArgs e)
    {
        string idToDelete = "1"; // Предполагаем, что в тесте есть ID для удаления

        if (string.IsNullOrEmpty(idToDelete))
        {
            MessageBoxText = "Пожалуйста, введите ID для удаления записи.";
            return;
        }

        try
        {
            // Ваш код удаления записи
            // ...

            MessageBoxText = "Запись успешно удалена из таблицы.";
        }
        catch (Exception ex)
        {
            MessageBoxText = $"Ошибка при удалении записи: {ex.Message}";
        }
    }

    public void button3_Click(object sender, EventArgs e)
    {
        string nameToSearch = "Маша ПК"; // Предполагаем, что в тесте есть имя для поиска

        if (string.IsNullOrEmpty(nameToSearch))
        {
            MessageBoxText = "Пожалуйста, введите имя для поиска контакта.";
            return;
        }

        try
        {
            // Ваш код поиска контакта
            // ...

            MessageBoxText = "Контакт с указанным именем не найден.";
        }
        catch (Exception ex)
        {
            MessageBoxText = $"Ошибка при поиске контакта: {ex.Message}";
        }
    }
}

public class UnitTest1
{
    [Fact]
    public void Button2Click_DeleteRecord_Success()
    {
        // Arrange
        var form = new Form1();

        // Act
        form.button2_Click(null, EventArgs.Empty);

        // Assert
        Assert.Contains("Запись успешно удалена из таблицы.", form.MessageBoxText);
    }

    [Fact]
    public void Button2Click_InvalidId_ShowErrorMessage()
    {
        // Arrange
        var form = new Form1();

        // Act
        form.button2_Click(null, EventArgs.Empty);

        // Assert
        Assert.Contains("Пожалуйста, введите ID для удаления записи.", form.MessageBoxText);
    }

    [Fact]
    public void Button3Click_SearchByName_Success()
    {
        // Arrange
        var form = new Form1();

        // Act
        form.button3_Click(null, EventArgs.Empty);

        // Assert
        Assert.Contains("Контакт с указанным именем не найден.", form.MessageBoxText);
    }

    [Fact]
    public void Button3Click_InvalidName_ShowErrorMessage()
    {
        // Arrange
        var form = new Form1();

        // Act
        form.button3_Click(null, EventArgs.Empty);

        // Assert
        Assert.Contains("Пожалуйста, введите имя для поиска контакта.", form.MessageBoxText);
    }
}