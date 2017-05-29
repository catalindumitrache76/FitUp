Imports System.Data
Imports System.Data.SqlClient

Public Class MainScreen

    Dim AddMealCategory As New ComboBox
    Dim AddAMealDoneButton As New Button
    Dim MealDescriptionTextbox As New TextBox

    Dim AddAnExerciseCategory As New ComboBox
    Dim AddAnExerciseDoneButton As New Button
    Dim ExerciseDescriptionTextbox As New TextBox

    Private myConn As SqlConnection

    Dim standardMealButton As New Button
    Dim standardExerciseButton As New Button
    Private Sub MainScreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SplitContainer3.SplitterDistance = AddAMealButton.Height
        SplitContainer4.SplitterDistance = AddAnExerciseButton.Height
        myConn = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\FitUp\FitUp\BaseDatosFitUp.mdf;Integrated Security=True;Connect Timeout=5")
        myConn.Open()
        standardMealButton = AddAMealButton
        standardExerciseButton = AddAnExerciseButton


    End Sub


    Private Sub MainScreen_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize

        Dim myControl As Control
        myControl = sender

        Try
            SplitContainer1.SplitterDistance = myControl.Size.Width / 3
            SplitContainer2.SplitterDistance = myControl.Size.Width / 3
            ' For meals
            AddMealCategory.Width = (myControl.Size.Width / 3) - 10
            MealDescriptionTextbox.Width = (myControl.Size.Width / 3) - 10
            MealDescriptionTextbox.Height = SplitContainer3.SplitterDistance - (AddAMealButton.Height * 3)

            ' For meals
            AddAnExerciseCategory.Width = (myControl.Size.Width / 3) - 10
            ExerciseDescriptionTextbox.Width = (myControl.Size.Width / 3) - 10
            ExerciseDescriptionTextbox.Height = SplitContainer4.SplitterDistance - (AddAnExerciseButton.Height * 3)

        Catch ex As InvalidOperationException
            Console.WriteLine("Error producido por minimizar la pantalla")
        End Try
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles AddAMealButton.Click

        'Expand the container 
        SplitContainer3.SplitterDistance = 170 + standardMealButton.Height

        'Add the combobox
        AddMealCategory.DropDownStyle = ComboBoxStyle.DropDownList
        AddMealCategory.Font = AddAMealButton.Font
        AddMealCategory.Location = New Point(AddAMealButton.Location.X, AddAMealButton.Location.Y + AddAMealButton.Height)
        AddMealCategory.Size = AddAMealButton.Size
        AddMealCategory.BackColor = Color.White
        AddMealCategory.Items.Add("Choose a category")
        AddMealCategory.Items.Add("Breakfast")
        AddMealCategory.Items.Add("12 a.m.")
        AddMealCategory.Items.Add("Lunch")
        AddMealCategory.Items.Add("5 p.m.")
        AddMealCategory.Items.Add("Dinner")
        AddMealCategory.SelectedIndex = 0

        'Add the textbox
        MealDescriptionTextbox.Font = AddAMealButton.Font
        MealDescriptionTextbox.Multiline = True
        MealDescriptionTextbox.Location = New Point(AddAMealButton.Location.X, AddAMealButton.Location.Y + (AddAMealButton.Height * 2))
        MealDescriptionTextbox.Size = New Size(AddAMealButton.Width, 170 - (AddAMealButton.Height * 2))

        'Add the done button
        AddAMealDoneButton.Dock = DockStyle.Bottom
        AddAMealDoneButton.Size = AddAMealButton.Size
        AddAMealDoneButton.Font = AddAMealButton.Font
        AddAMealDoneButton.BackColor = Color.LightGreen
        AddAMealDoneButton.Text = "Done"

        'Add the controllers to the panel
        SplitContainer3.Panel1.Controls.Add(AddMealCategory)
        SplitContainer3.Panel1.Controls.Add(MealDescriptionTextbox)
        SplitContainer3.Panel1.Controls.Add(AddAMealDoneButton)

        'Disables the AddAMealButton
        AddAMealButton.Enabled = False

        AddHandler AddAMealDoneButton.Click, AddressOf AddAMealDoneButton_Click

    End Sub

    Private Sub AddAnExerciseButton_Click(sender As Object, e As EventArgs) Handles AddAnExerciseButton.Click
        'Expand the container 
        SplitContainer4.SplitterDistance = 170 + standardExerciseButton.Height

        'Add the combobox
        AddAnExerciseCategory.DropDownStyle = ComboBoxStyle.DropDownList
        AddAnExerciseCategory.Font = standardExerciseButton.Font
        AddAnExerciseCategory.Location = New Point(standardExerciseButton.Location.X, standardExerciseButton.Location.Y + standardExerciseButton.Height)
        AddAnExerciseCategory.Size = standardExerciseButton.Size
        AddAnExerciseCategory.BackColor = Color.White
        AddAnExerciseCategory.Items.Add("Choose a category")
        AddAnExerciseCategory.Items.Add("Back")
        AddAnExerciseCategory.Items.Add("Biceps")
        AddAnExerciseCategory.Items.Add("Triceps")
        AddAnExerciseCategory.Items.Add("Abs")
        AddAnExerciseCategory.Items.Add("Cuadriceps")
        AddAnExerciseCategory.SelectedIndex = 0

        'Add the textbox
        ExerciseDescriptionTextbox.Font = standardExerciseButton.Font
        ExerciseDescriptionTextbox.Multiline = True
        ExerciseDescriptionTextbox.Location = New Point(standardExerciseButton.Location.X, standardExerciseButton.Location.Y + (standardExerciseButton.Height * 2))
        ExerciseDescriptionTextbox.Size = New Size(standardExerciseButton.Width, 170 - (standardExerciseButton.Height * 2))


        'Add the done button
        AddAnExerciseDoneButton.Dock = DockStyle.Bottom
        AddAnExerciseDoneButton.Size = standardExerciseButton.Size
        AddAnExerciseDoneButton.Font = standardExerciseButton.Font
        AddAnExerciseDoneButton.BackColor = Color.LightGreen
        AddAnExerciseDoneButton.Text = "Done"

        'Add the controllers to the panel
        SplitContainer4.Panel1.Controls.Add(AddAnExerciseCategory)
        SplitContainer4.Panel1.Controls.Add(ExerciseDescriptionTextbox)
        SplitContainer4.Panel1.Controls.Add(AddAnExerciseDoneButton)

        'Disables the AddAnExerciseButton
        AddAnExerciseButton.Enabled = False

        AddHandler AddAnExerciseDoneButton.Click, AddressOf AddAnExerciseDoneButton_Click

    End Sub

    Private Sub AddAMealDoneButton_Click(sender As Object, e As EventArgs)

        If AddMealCategory.SelectedIndex = 0 Or MealDescriptionTextbox.Text Is "" Then
            MsgBox("You have to choose a category and provide a description for the meal")
        Else
            'Inserts a new Meal in the database
            insertNewMeal(MealDescriptionTextbox.Text, AddMealCategory.SelectedItem)

            'Removes the rest of the buttons
            removeAddMealButtons()

            'Get the number of the meals in the database
            getMealsNumber()
        End If

    End Sub

    Private Sub AddAnExerciseDoneButton_Click(sender As Object, e As EventArgs)

        If AddAnExerciseCategory.SelectedIndex = 0 Or ExerciseDescriptionTextbox.Text Is "" Then
            MsgBox("You have to choose a category and enter a name for the exercise")
        Else
            'Inserts a new Exercise in the database
            insertNewExercise(ExerciseDescriptionTextbox.Text, "imageHere", AddAnExerciseCategory.SelectedItem)

            'Removes the rest of the buttons
            removeAddExerciseButtons()

            'Get the number of the exercises in the database
            getExerciseNumber()
        End If

    End Sub



    Private Function getExerciseNumber()
        Dim exercisesNumber = -1
        Try

            Dim myCmd As SqlCommand
            myCmd = New SqlCommand("SELECT Count(*) FROM Exercise", myConn)
            exercisesNumber = myCmd.ExecuteScalar()
            'MsgBox("There are " + exercisesNumber.ToString + " exercises in the database")

        Catch e As Exception
            Throw e
        End Try
        Return exercisesNumber
    End Function

    Private Sub removeAddMealButtons()
        'Removes the CategoryMeal, MealDescriptionTextbox and the AddAMealDoneButton
        SplitContainer3.Panel1.Controls.Remove(AddMealCategory)
        AddMealCategory = New ComboBox
        SplitContainer3.Panel1.Controls.Remove(MealDescriptionTextbox)
        MealDescriptionTextbox = New TextBox
        SplitContainer3.Panel1.Controls.Remove(AddAMealDoneButton)
        AddAMealDoneButton = New Button
        SplitContainer3.SplitterDistance = AddAMealButton.Height

        'Enables the AddAMealButton
        AddAMealButton.Enabled = True
    End Sub

    Private Sub removeAddExerciseButtons()
        'Removes the CategoryMeal, MealDescriptionTextbox and the AddAMealDoneButton
        SplitContainer4.Panel1.Controls.Remove(AddAnExerciseCategory)
        AddAnExerciseCategory = New ComboBox
        SplitContainer4.Panel1.Controls.Remove(ExerciseDescriptionTextbox)
        ExerciseDescriptionTextbox = New TextBox
        SplitContainer4.Panel1.Controls.Remove(AddAnExerciseDoneButton)
        AddAnExerciseDoneButton = New Button
        SplitContainer4.SplitterDistance = standardExerciseButton.Height

        'Enables the AddAnExerciseButton
        AddAnExerciseButton.Enabled = True
    End Sub

    Private Sub insertNewMeal(name As String, category As String)
        Try

            Dim myCmd As SqlCommand
            myCmd = New SqlCommand("INSERT INTO Meal (name, category) values (@name, @category)", myConn)
            myCmd.Parameters.AddWithValue("@name", name)
            myCmd.Parameters.AddWithValue("@category", category)
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

    Private Sub insertNewExercise(name As String, image As String, category As Object)
        Try

            Dim myCmd As SqlCommand
            myCmd = New SqlCommand("INSERT INTO Exercise (name, image, category) values (@name, @image, @category)", myConn)
            myCmd.Parameters.AddWithValue("@name", name)
            myCmd.Parameters.AddWithValue("@image", image)
            myCmd.Parameters.AddWithValue("@category", category)
            myCmd.ExecuteNonQuery()

            MsgBox("Your exercise has been saved.")

        Catch e As Exception

            If e.Message.StartsWith("Violation of PRIMARY KEY constraint") Then
                ' handle your violation
                MsgBox("The exercise you inserted already exists in the database")
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
            getMealsNumber()
            removeAddExerciseButtons()
        ElseIf myControl.SelectedTab.Text = "My exercises" Then
            getExerciseNumber()
            removeAddMealButtons()
        Else
            removeAddMealButtons()
            removeAddExerciseButtons()
        End If

    End Sub

    Private Function getMealsNumber() As Object
        Dim mealsNumber = -1
        Try

            Dim myCmd As SqlCommand
            myCmd = New SqlCommand("SELECT Count(*) FROM Meal", myConn)
            mealsNumber = myCmd.ExecuteScalar()
            'MsgBox("There are " + mealsNumber.ToString + " meals in the database")

        Catch e As Exception
            Throw e
        End Try
        Return mealsNumber
    End Function


End Class
