Imports System.Data
Imports System.Data.SqlClient

Public Class MainScreen

    Dim AddAMealDoneButton As New Button
    Dim MealDescriptionTextbox As New TextBox
    Private myConn As SqlConnection


    Private Sub MainScreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SplitContainer3.SplitterDistance = AddAMealButton.Height
        myConn = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\FitUp\FitUp\BaseDatosFitUp.mdf;Integrated Security=True;Connect Timeout=5")
        myConn.Open()
    End Sub


    Private Sub MainScreen_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize

        Dim myControl As Control
        myControl = sender
        'If MyBase.MinimizeBox Then
        'Else

        SplitContainer1.SplitterDistance = myControl.Size.Width / 3
        SplitContainer2.SplitterDistance = myControl.Size.Width / 3
        MealDescriptionTextbox.Width = (myControl.Size.Width / 3) - 6
        Dim AddAMealDoneButtonLocation = New Point(
            MealDescriptionTextbox.Location.X, SplitContainer3.SplitterDistance - AddAMealButton.Height)
        MealDescriptionTextbox.Height = SplitContainer3.SplitterDistance - (AddAMealButton.Height * 2)


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles AddAMealButton.Click

        'Expand the container and add a textbox with a button
        SplitContainer3.SplitterDistance = 150
        MealDescriptionTextbox.Font = AddAMealButton.Font
        MealDescriptionTextbox.Multiline = True

        AddAMealDoneButton.Dock = DockStyle.Bottom
        AddAMealDoneButton.Size = AddAMealButton.Size
        AddAMealDoneButton.Font = AddAMealButton.Font
        AddAMealDoneButton.BackColor = Color.LightGreen
        AddAMealDoneButton.Text = "Done"
        Dim MealDescriptionLocation = New Point(AddAMealButton.Location.X, AddAMealButton.Location.Y + AddAMealButton.Height)
        MealDescriptionTextbox.Location = MealDescriptionLocation
        MealDescriptionTextbox.Size = New Size(AddAMealButton.Width, 150 - (AddAMealButton.Height * 2))

        SplitContainer3.Panel1.Controls.Add(MealDescriptionTextbox)
        SplitContainer3.Panel1.Controls.Add(AddAMealDoneButton)

        AddHandler AddAMealDoneButton.Click, AddressOf AddAMealDoneButton_Click

    End Sub

    Private Sub AddAMealDoneButton_Click(sender As Object, e As EventArgs)
        If MealDescriptionTextbox.Text Is "" Then
            MsgBox("You have to provide a description for the meal")
        Else
            'Inserts a new Meal in the database
            insertNewMeal(MealDescriptionTextbox.Text, "Categoria", "Planeado")

            'Removes the MealDescriptionTextbox and the AddAMealDoneButton
            SplitContainer3.Panel1.Controls.Remove(MealDescriptionTextbox)
            MealDescriptionTextbox = New TextBox
            SplitContainer3.Panel1.Controls.Remove(AddAMealDoneButton)
            AddAMealDoneButton = New Button
            SplitContainer3.SplitterDistance = AddAMealButton.Height

            'Get the number of the meals in the database
            getMeals()

        End If
    End Sub





    Private Sub insertNewMeal(name As String, category As String, state As String)
        Try

            Dim myCmd As SqlCommand
            myCmd = New SqlCommand("INSERT INTO Meal (name, category, state) values (@name, @category, @state)", myConn)
            myCmd.Parameters.AddWithValue("@name", name)
            myCmd.Parameters.AddWithValue("@category", category)
            myCmd.Parameters.AddWithValue("@state", state)
            myCmd.ExecuteNonQuery()

            MsgBox("Your meal has been saved.")

        Catch e As Exception

            If e.Message.StartsWith("Violation of PRIMARY KEY constraint") Then
                ' handle your violation
                MsgBox("The meal you inserted already exists in the database")
            Else
                're-throw the error if you haven't handled it
                Throw e
            End If
        End Try

    End Sub

    Private Sub TabControl3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl3.SelectedIndexChanged

        Dim myControl As TabControl
        myControl = sender
        If myControl.SelectedTab.Text = "My meals" Then
            getMeals()
        End If

    End Sub

    Private Function getMeals() As Object
        Dim mealsNumber = -1
        Try

            Dim myCmd As SqlCommand
            myCmd = New SqlCommand("SELECT Count(*) FROM Meal", myConn)
            mealsNumber = myCmd.ExecuteScalar()
            MsgBox("There are " + mealsNumber.ToString + " meals in the database")
        Catch e As Exception

            If e.Message.StartsWith("Violation of PRIMARY KEY constraint") Then
                ' handle your violation
                MsgBox("The meal you inserted already exists in the database")
            Else
                're-throw the error if you haven't handled it
                Throw e
            End If
        End Try
        Return mealsNumber
    End Function
End Class
