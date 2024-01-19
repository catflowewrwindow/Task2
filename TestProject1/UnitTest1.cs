using System;
using Xunit;


public class Form1
{
    public string MessageBoxText { get; private set; } = "";

    public void button2_Click(object sender, EventArgs e)
    {
        string idToDelete = "1"; // ������������, ��� � ����� ���� ID ��� ��������

        if (string.IsNullOrEmpty(idToDelete))
        {
            MessageBoxText = "����������, ������� ID ��� �������� ������.";
            return;
        }

        try
        {
            // ��� ��� �������� ������
            // ...

            MessageBoxText = "������ ������� ������� �� �������.";
        }
        catch (Exception ex)
        {
            MessageBoxText = $"������ ��� �������� ������: {ex.Message}";
        }
    }

    public void button3_Click(object sender, EventArgs e)
    {
        string nameToSearch = "���� ��"; // ������������, ��� � ����� ���� ��� ��� ������

        if (string.IsNullOrEmpty(nameToSearch))
        {
            MessageBoxText = "����������, ������� ��� ��� ������ ��������.";
            return;
        }

        try
        {
            // ��� ��� ������ ��������
            // ...

            MessageBoxText = "������� � ��������� ������ �� ������.";
        }
        catch (Exception ex)
        {
            MessageBoxText = $"������ ��� ������ ��������: {ex.Message}";
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
        Assert.Contains("������ ������� ������� �� �������.", form.MessageBoxText);
    }

    [Fact]
    public void Button2Click_InvalidId_ShowErrorMessage()
    {
        // Arrange
        var form = new Form1();

        // Act
        form.button2_Click(null, EventArgs.Empty);

        // Assert
        Assert.Contains("����������, ������� ID ��� �������� ������.", form.MessageBoxText);
    }

    [Fact]
    public void Button3Click_SearchByName_Success()
    {
        // Arrange
        var form = new Form1();

        // Act
        form.button3_Click(null, EventArgs.Empty);

        // Assert
        Assert.Contains("������� � ��������� ������ �� ������.", form.MessageBoxText);
    }

    [Fact]
    public void Button3Click_InvalidName_ShowErrorMessage()
    {
        // Arrange
        var form = new Form1();

        // Act
        form.button3_Click(null, EventArgs.Empty);

        // Assert
        Assert.Contains("����������, ������� ��� ��� ������ ��������.", form.MessageBoxText);
    }
}