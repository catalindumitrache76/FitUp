Imports System.Data
Imports System.Data.SqlClient

Public Class MainScreen

    Dim AddAMealDoneButton As New Button
    Dim MealDescriptionTextbox As New TextBox
    Private myConn As SqlConnection
    Private myCmd As SqlCommand
    Private myReader As SqlDataReader

    Private Sub MainScreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SplitContainer3.SplitterDistance = AddAMealButton.Height
        ConnectToDatabase()
    End Sub

    Private Sub ConnectToDatabase()


        '' FIND INFO HERE: https://msdn.microsoft.com/en-us/library/ms233812.aspx
        ''Create a New row.
        'Dim newRegionRow As NorthwindDataSet.RegionRow
        'newRegionRow = Me.NorthwindDataSet._Region.NewRegionRow()
        'newRegionRow.RegionID = 5
        'newRegionRow.RegionDescription = "NorthWestern"

        '' Add the row to the Region table
        'Me.NorthwindDataSet._Region.Rows.Add(newRegionRow)

        '' Save the new row to the database
        'Me.RegionTableAdapter.Update(Me.NorthwindDataSet._Region)




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
            SplitContainer3.Panel1.Controls.Remove(MealDescriptionTextbox)
            MealDescriptionTextbox = New TextBox
            SplitContainer3.Panel1.Controls.Remove(AddAMealDoneButton)
            AddAMealDoneButton = New Button
            SplitContainer3.SplitterDistance = AddAMealButton.Height
            'Create a Command object.
            myCmd = myConn.CreateCommand

            myCmd.CommandText = "INSERT INTO Meal (name, category, state) 
                VALUES (" + MealDescriptionTextbox.Text + ", ""Breakfast"", ""planned"");"

            myCmd.ExecuteNonQuery()


            MsgBox("Your meal has been saved.")
        End If
    End Sub
End Class
