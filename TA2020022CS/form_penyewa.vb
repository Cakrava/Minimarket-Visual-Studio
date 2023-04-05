﻿Public Class form_penyewa

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Dispose()
        beranda.mobil.Enabled = True
        beranda.penyewa.Enabled = True
        beranda.penyewaan.Enabled = True
        beranda.kembali.Enabled = True
        beranda.lap_sewa.Enabled = True
        beranda.lap_mobil.Enabled = True
        beranda.lap_kembali.Enabled = True
        beranda.lap_penyewa.Enabled = True
        beranda.btn_user.Enabled = True
        beranda.keluar.Enabled = True
    End Sub
    Sub bersih()
        txtId.Clear()
        txtNama.Clear()
        txtnohp.Clear()
    End Sub

    Sub setdgv()
        'mengatur judul kolom
        dgv.Columns(0).HeaderText = "ID Penyewa"
        dgv.Columns(1).HeaderText = "Nama Penyewa"
        dgv.Columns(2).HeaderText = "Nomor Hp"
   

        dgv.Columns(0).Width = 80
        dgv.Columns(1).Width = 80
        dgv.Columns(2).Width = 80
     

    End Sub
    Sub buatTombol()
        'buat tombol edit di dlm dgv
        Dim btnUpdate As New DataGridViewButtonColumn
        btnUpdate.Name = "btnUpdate"
        btnUpdate.HeaderText = ""
        btnUpdate.FlatStyle = FlatStyle.Popup
        btnUpdate.DefaultCellStyle.BackColor = Color.Aqua
        btnUpdate.Text = "Edit"
        btnUpdate.Width = 50
        btnUpdate.UseColumnTextForButtonValue = True
        dgv.Columns.Add(btnUpdate)

        'buat tombol hapus didalam dgv
        Dim btnHapus As New DataGridViewButtonColumn
        btnHapus.Name = "btnHapus"
        btnHapus.HeaderText = ""
        btnHapus.FlatStyle = FlatStyle.Popup
        btnHapus.DefaultCellStyle.BackColor = Color.Red
        btnHapus.Text = "Hapus"
        btnHapus.Width = 50
        btnHapus.UseColumnTextForButtonValue = True
        dgv.Columns.Add(btnHapus)
    End Sub

    Sub tampilData(ByVal sql As String)
        kon.Open()
        perintah.Connection = kon
        perintah.CommandType = CommandType.Text
        perintah.CommandText = sql
        mda.SelectCommand = perintah
        ds.Tables.Clear()
        mda.Fill(ds, "data")
        dgv.DataSource = ds.Tables("data")
        kon.Close()

    End Sub

    Private Sub form_penyewa_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Button1.Enabled = False
        Button2.Enabled = False
        dgv.Columns.Clear()
        Call tampilData("select id_penyewa, nama_penyewa, nohp from penyewa")
        Call setdgv()
        Call buatTombol()
    End Sub

    Private Sub dgv_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellContentClick
        Dim i As Integer
        Dim id As String
        i = dgv.CurrentRow.Index
        id = CStr(dgv.Rows.Item(i).Cells(0).Value)
        'jika diklik tombol Update
        If e.ColumnIndex = 3 Then
            txtId.Text = id
            txtNama.Text = CStr(dgv.Rows.Item(i).Cells(1).Value)
            txtnohp.Text = CStr(dgv.Rows.Item(i).Cells(2).Value)

            Button2.Enabled = True
            Button1.Enabled = False

        End If
        'jika diklik tombol hapus
        If e.ColumnIndex = 4 Then
            Dim x As Byte
            x = CByte(MsgBox("Hapus data dengan ID " + id, CType(MsgBoxStyle.Critical + vbYesNo, MsgBoxStyle), "Konfirmasi"))
            If x = vbYes Then
                kon.Open()
                perintah.Connection = kon
                perintah.CommandType = CommandType.Text
                perintah.CommandText = "delete from penyewa where id_penyewa = '" & id & "'"
                perintah.ExecuteNonQuery()
                kon.Close()

                'panggil even form load
                form_penyewa_Load(e, CType(AcceptButton, EventArgs))
            End If
        End If

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        kon.Open()
        perintah.Connection = kon
        perintah.CommandType = CommandType.Text
        perintah.CommandText = "update penyewa set nama_penyewa='" & txtNama.Text & "', nohp='" & txtnohp.Text & "' where id_penyewa='" & txtId.Text & "'"
        perintah.ExecuteNonQuery()
        kon.Close()
        Call bersih()
        form_penyewa_Load(e, CType(AcceptButton, EventArgs))

        MsgBox("Data Berhasil Diupdate", MsgBoxStyle.Information, "Informasi")
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Button1.Enabled = True
        txtId.Focus()
        Call bersih()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        kon.Open()
        perintah.Connection = kon
        perintah.CommandType = CommandType.Text
        perintah.CommandText = "insert into penyewa (id_penyewa, nama_penyewa, nohp) values ('" & txtId.Text & "','" & txtNama.Text & "','" & txtnohp.Text & "')"
        perintah.ExecuteNonQuery()
        kon.Close()
        Call bersih()
        form_penyewa_Load(e, CType(AcceptButton, EventArgs))

        MsgBox("Data Berhasil Disimpan", MsgBoxStyle.Information, "Informasi")

    End Sub

    
    Private Sub txtCari_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCari.TextChanged
        dgv.Columns.Clear()

        Call tampilData("select id_penyewa,nama_penyewa, nohp from penyewa where id_penyewa like '%" & txtCari.Text & "%' or nama_penyewa like '%" & txtCari.Text & "%' ")
        Call setdgv()
        Call buatTombol()
    End Sub
End Class