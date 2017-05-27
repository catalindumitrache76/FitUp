Partial Class BaseDatosFitUpDataSet
    Partial Public Class ExerciseDataTable
        Private Sub ExerciseDataTable_ColumnChanging(sender As Object, e As DataColumnChangeEventArgs) Handles Me.ColumnChanging


        End Sub

    End Class

    Partial Public Class MealDataTable
        Private Sub MealDataTable_MealRowChanging(sender As Object, e As MealRowChangeEvent) Handles Me.MealRowChanging

        End Sub

    End Class

    Partial Public Class DayDataTable
        Private Sub DayDataTable_DayRowChanging(sender As Object, e As DayRowChangeEvent) Handles Me.DayRowChanging

        End Sub

        Private Sub DayDataTable_ColumnChanging(sender As Object, e As DataColumnChangeEventArgs) Handles Me.ColumnChanging


        End Sub

    End Class
End Class
