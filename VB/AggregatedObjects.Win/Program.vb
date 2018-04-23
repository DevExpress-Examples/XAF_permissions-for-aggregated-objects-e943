Imports System
Imports System.Configuration
Imports System.Windows.Forms

Imports DevExpress.Persistent.Base
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Security
Imports DevExpress.ExpressApp.Win

Imports AggregatedObjects.Win

Public Class Program

    Private Shared Sub WinApplication_DatabaseVersionMismatch(ByVal sender As Object, ByVal args As DatabaseVersionMismatchEventArgs)
        If System.Diagnostics.Debugger.IsAttached Then
            args.Updater.Update()
            args.Handled = True
        End If
    End Sub
    <STAThread()> _
    Public Shared Sub Main(ByVal arguments() As String)
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
        EditModelPermission.AlwaysGranted = System.Diagnostics.Debugger.IsAttached
        Dim _application As AggregatedObjectsWindowsFormsApplication = New AggregatedObjectsWindowsFormsApplication()

        'Uncomment the following line to allow Sam access the MyAddress objects.
        'ObjectAccessComparer.SetCurrentComparer(New ObjectAccessComparer(ObjectAccessCompareMode.Strict))

        If (Not ConfigurationManager.ConnectionStrings.Item("ConnectionString") Is Nothing) Then
            _application.ConnectionString = ConfigurationManager.ConnectionStrings.Item("ConnectionString").ConnectionString
        End If
        If System.Diagnostics.Debugger.IsAttached Then
            _application.DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways
        End If
        Try
            _application.Setup()
            _application.Start()
        Catch e As Exception
            _application.HandleException(e)
        End Try

    End Sub
End Class
