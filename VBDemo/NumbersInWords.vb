Public Class NumbersInWords

	'Private WholeNumberW1(0 To 19) As String
	'Private WholeNumberW20T90(2 To 9) As String
	'Private WholeNumberW100(1 To 9) As String
	'Private WholeNumberW1000(1 To 2) As String, WholeNumberW1000S As String

	''Private WholeNumberW10 As String


	Private Sub WholeNInWords()
		'WholeNumberW1(0) = "нула"
		'WholeNumberW1(1) = "един"
		'WholeNumberW1(2) = "два"
		'WholeNumberW1(3) = "три"
		'WholeNumberW1(4) = "четири"
		'WholeNumberW1(5) = "пет"
		'WholeNumberW1(6) = "шест"
		'WholeNumberW1(7) = "седем"
		'WholeNumberW1(8) = "осем"
		'WholeNumberW1(9) = "девет"
		'WholeNumberW1(10) = "десет"
		'WholeNumberW1(11) = "единадесет"
		'WholeNumberW1(12) = "дванадесет"
		'WholeNumberW1(13) = "тринадесет"
		'WholeNumberW1(14) = "четиринадесет"
		'WholeNumberW1(15) = "петнадесет"
		'WholeNumberW1(16) = "шестнадесет"
		'WholeNumberW1(17) = "седемнадесет"
		'WholeNumberW1(18) = "осемнадест"
		'WholeNumberW1(19) = "деветнадест"

		'WholeNumberW20T90(2) = "двадесет"
		'WholeNumberW20T90(3) = "тридесет"
		'WholeNumberW20T90(4) = "четиридесет"
		'WholeNumberW20T90(5) = "петдесет"
		'WholeNumberW20T90(6) = "шестдесет"
		'WholeNumberW20T90(7) = "седемдесет"
		'WholeNumberW20T90(8) = "осемдесет"
		'WholeNumberW20T90(9) = "деветдесет"

		'WholeNumberW100(1) = "сто"
		'WholeNumberW100(2) = "двеста"
		'WholeNumberW100(3) = "триста"
		'WholeNumberW100(4) = "четиристотин"
		'WholeNumberW100(5) = "петстотин"
		'WholeNumberW100(6) = "шестстотин"
		'WholeNumberW100(7) = "седемстотин"
		'WholeNumberW100(8) = "осемстотин"
		'WholeNumberW100(9) = "деветстотин"

		'WholeNumberW1000(1) = "хиляда"
		'WholeNumberW1000(2) = "две хиляди"
		'WholeNumberW1000S = "хиляди"
	End Sub


	Public Function TotalInWords(ByVal Ssuma99 As Double) As String  '(ByVal nikSumGeneral As Double)
		WholeNInWords()

		Dim TsialaChast As Long
		Dim TsialaChastStr As String
		Dim TsialaChastStr11 As String
		Dim InvSumaT As Double

		InvSumaT = CDbl(Format(Ssuma99, "####0.00"))
		TsialaChast = Math.Abs(Fix(InvSumaT))

		TsialaChastStr = CStr(Math.Abs(TsialaChast))

		TsialaChastStr11 = InWordsSum(TsialaChastStr)

		TotalInWords = UCase(Left(TsialaChastStr11, 1)) + Mid(TsialaChastStr11, 2) + " лв. и " + CStr(Right(CStr(Format(InvSumaT, "##,##0.00")), 2)) + " ст."

		'Dim DrobnaChast As Integer
		'InvSumaT = DSum("[SumaT]", "[AInvoiceQuery]")
		'Format(5459.4, "##,##0.00")
		'MsgBox CStr(Fix(InvSumaT)) + " << >> " + CStr(Right(CStr(Format(InvSumaT, "##,##0.00")), 2))
		'InvSumaT = CDbl(Format(Report_ReportInvCopy.Text105, "####0.00"))
		'TsialaChast = Abs(InvSumaT) 'TsialaChast = Fix(InvSumaT)
	End Function

	Private Function InWordsSum(ByVal nikValueStr As String) As String
		'	Dim IaLen As Integer
		'	Dim IaLenTho As Integer
		'	Dim IaLenTho123 As Integer

		'	Dim yY As Integer
		'	Dim Xy As Integer
		'	Dim ThousS As Integer
		'	Dim nikV123 As String
		'	Dim Zzz As String

		'	'IaLen =  1 - до 9
		'	'IaLen =  2 - до 99
		'	'IaLen =  3 - до 999
		'	'IaLen =  4 - до 9 хиляди
		'	'IaLen =  5 - до 99 хиляди
		'	'IaLen =  6 - до 999 хиляди
		'	'IaLen =  7 - до 9 милиона
		'	'IaLen =  8 - до 99 милиона
		'	'IaLen =  9 - до 999 милиона
		'	'IaLen = 10 - до 9 милиарда ;;; IaLen = 11 - до 99 милиарда
		'	'<<< Да тръгна от единиците в нарастваща посока. >>>

		'	IaLen = Len(nikValueStr)
		'	Zzz = ""

		'	Select Case IaLen
		'		Case 1
		'			yY = Int(Mid(nikValueStr, 1, 1))
		'			Zzz = WholeNumberW1(yY)
		'		Case 2
		'			yY = Int(Mid(nikValueStr, 1, 1))
		'			Xy = Int(Mid(nikValueStr, 1, 2))
		'			If yY < 2 Then
		'				Zzz = WholeNumberW1(Xy)
		'			ElseIf yY >= 2 And Int(Mid(nikValueStr, 2, 1)) <> 0 Then
		'				Zzz = WholeNumberW20T90(Int(Mid(nikValueStr, 1, 1))) + " и " + WholeNumberW1(Int(Mid(nikValueStr, 2, 1)))
		'			Else
		'				Zzz = WholeNumberW20T90(Int(Mid(nikValueStr, 1, 1)))
		'			End If
		'		Case 3
		'			Zzz = WholeNumberW100(Int(Mid(nikValueStr, 1, 1)))
		'			yY = Int(Mid(nikValueStr, 2, 1))
		'			Xy = Int(Mid(nikValueStr, 2, 2))

		'			'MsgBox CStr(yY) + " << >> " + CStr(Xy)
		'			If Int(Mid(nikValueStr, 2, 1)) = 0 And Int(Mid(nikValueStr, 3, 1)) = 0 Then
		'				Zzz = Zzz
		'			ElseIf yY < 2 And Int(Mid(nikValueStr, 3, 1)) >= 0 Then
		'				Zzz = Zzz + " и " + WholeNumberW1(Xy)
		'			ElseIf yY >= 2 And Int(Mid(nikValueStr, 3, 1)) <> 0 Then
		'				Zzz = Zzz + "," + WholeNumberW20T90(Int(Mid(nikValueStr, 2, 1))) + " и " + WholeNumberW1(Int(Mid(nikValueStr, 3, 1)))
		'			ElseIf yY >= 2 And Int(Mid(nikValueStr, 3, 1)) = 0 Then
		'				Zzz = Zzz + " и " + WholeNumberW20T90(Int(Mid(nikValueStr, 2, 1)))
		'				'        Else
		'				'            zZZ = zZZ
		'			End If
		'		Case 4 To 6
		'			IaLenTho = Int(Len(nikValueStr)) - Int(Len(Right(nikValueStr, 3)))
		'			Select Case IaLenTho
		'				Case 1
		'					ThousS = Int(Mid(nikValueStr, 1, 1))
		'					If ThousS >= 3 Then
		'						Zzz = WholeNumberW1(ThousS) + " " + WholeNumberW1000S
		'					ElseIf ThousS < 3 Then
		'						Zzz = WholeNumberW1000(ThousS)
		'					End If
		'				Case 2
		'					yY = Int(Mid(nikValueStr, 1, 1))
		'					Xy = Int(Mid(nikValueStr, 1, 2))
		'					If yY < 2 Then
		'						Zzz = WholeNumberW1(Xy) + " " + WholeNumberW1000S
		'					ElseIf yY >= 2 And Int(Mid(nikValueStr, 2, 1)) <> 0 Then
		'						Zzz = WholeNumberW20T90(Int(Mid(nikValueStr, 1, 1))) + " и " + WholeNumberW1(Int(Mid(nikValueStr, 2, 1))) +
		'						" " + WholeNumberW1000S
		'					Else
		'						Zzz = WholeNumberW20T90(Int(Mid(nikValueStr, 1, 1))) + " " + WholeNumberW1000S
		'					End If
		'				Case 3
		'					Zzz = WholeNumberW100(Int(Mid(nikValueStr, 1, 1)))
		'					yY = Int(Mid(nikValueStr, 2, 1))
		'					Xy = Int(Mid(nikValueStr, 2, 2))

		'					If yY < 2 And Int(Mid(nikValueStr, 3, 1)) <> 0 Then
		'						If Int(Mid(nikValueStr, 3, 1)) = 1 Then
		'							Zzz = Zzz + " и една"
		'						ElseIf Int(Mid(nikValueStr, 3, 1)) = 2 Then
		'							Zzz = Zzz + " и две"
		'						Else
		'							Zzz = Zzz + " и " + WholeNumberW1(Xy)
		'						End If

		'					ElseIf yY >= 2 And Int(Mid(nikValueStr, 3, 1)) <> 0 Then
		'						Zzz = Zzz + "," + WholeNumberW20T90(Int(Mid(nikValueStr, 2, 1))) + " и " + WholeNumberW1(Int(Mid(nikValueStr, 3, 1)))
		'					ElseIf yY >= 2 And Int(Mid(nikValueStr, 3, 1)) = 0 Then
		'						Zzz = Zzz + " и " + WholeNumberW20T90(Int(Mid(nikValueStr, 2, 1)))
		'					End If
		'					Zzz = Zzz + " " + WholeNumberW1000S
		'			End Select
		'			'MsgBox CStr(Abs(Right(nikValueStr, 3)))
		'			nikV123 = CStr(Math.Abs(Right(nikValueStr, 3)))
		'			IaLenTho123 = Len(nikV123) 'Len(Abs(Right(nikValueStr, 3)))
		'			Select Case IaLenTho123
		'				Case 1
		'					yY = Int(Mid(nikV123, 1, 1))
		'					If yY <> 0 Then Zzz = Zzz + " и " + WholeNumberW1(yY)
		'				Case 2
		'					yY = Int(Mid(nikV123, 1, 1))
		'					Xy = Int(Mid(nikV123, 1, 2))
		'					If yY < 2 Then
		'						Zzz = Zzz + " и " + WholeNumberW1(Xy)
		'					ElseIf yY >= 2 And Int(Mid(nikV123, 2, 1)) <> 0 Then
		'						Zzz = Zzz + "," + WholeNumberW20T90(Int(Mid(nikV123, 1, 1))) + " и " + WholeNumberW1(Int(Mid(nikV123, 2, 1)))
		'					Else
		'						Zzz = Zzz + " и " + WholeNumberW20T90(Int(Mid(nikV123, 1, 1)))
		'					End If
		'				Case 3
		'					If Int(Mid(nikV123, 2, 1)) = 0 And Int(Mid(nikV123, 3, 1)) = 0 Then
		'						Zzz = Zzz + " и " + WholeNumberW100(Int(Mid(nikV123, 1, 1)))
		'					Else
		'						Zzz = Zzz + "," + WholeNumberW100(Int(Mid(nikV123, 1, 1)))
		'						yY = Int(Mid(nikV123, 2, 1))
		'						Xy = Int(Mid(nikV123, 2, 2))

		'						If yY < 2 Then  'And Int(MID(nikV123, 3, 1)) <> 0
		'							Zzz = Zzz + " и " + WholeNumberW1(Xy)
		'						ElseIf yY >= 2 And Int(Mid(nikV123, 3, 1)) <> 0 Then
		'							Zzz = Zzz + "," + WholeNumberW20T90(Int(Mid(nikV123, 2, 1))) + " и " + WholeNumberW1(Int(Mid(nikV123, 3, 1)))
		'						Else
		'							Zzz = Zzz + " и " + WholeNumberW20T90(Int(Mid(nikV123, 2, 1)))
		'						End If
		'					End If
		'			End Select
		'		Case 7 To 9
		'			Zzz = MillionInWords(nikValueStr)
		'		Case Else
		'			Zzz = "Не е дифинирана с думи"
		'	End Select

		'	InWordsSum = Zzz
	End Function

	'Private Function MillionInWords(ByVal nikMillionV As String) As String
	'	Dim WholeNumberWMillion As String
	'	Dim WholeNumberWMillionSs As String
	'	Dim zZZMil As String
	'	Dim nikV456 As String
	'	Dim nikMil123 As String

	'	Dim IaLenThoM As Integer
	'	Dim IaLenTho456 As Integer
	'	Dim ThousMil As Integer
	'	Dim yYMil As Integer
	'	Dim XyMil As Integer
	'	Dim Million123 As Integer
	'	Dim IaLenMil123 As Integer

	'	WholeNumberWMillion = "милион" : WholeNumberWMillionSs = "милиона"

	'	IaLenThoM = Int(Len(nikMillionV)) - Int(Len(Right(nikMillionV, 6)))
	'	Select Case IaLenThoM
	'		Case 1
	'			Million123 = Int(Mid(nikMillionV, 1, 1))
	'			If Million123 >= 2 Then
	'				zZZMil = WholeNumberW1(Million123) + " " + WholeNumberWMillionSs
	'			Else
	'				zZZMil = WholeNumberW1(Million123) + " " + WholeNumberWMillion
	'			End If
	'		Case 2
	'			yYMil = Int(Mid(nikMillionV, 1, 1))
	'			XyMil = Int(Mid(nikMillionV, 1, 2))
	'			If yYMil < 2 Then
	'				zZZMil = WholeNumberW1(XyMil) + " " + WholeNumberWMillionSs
	'			ElseIf yYMil >= 2 And Int(Mid(nikMillionV, 2, 1)) <> 0 Then
	'				zZZMil = WholeNumberW20T90(Int(Mid(nikMillionV, 1, 1))) + " и " + WholeNumberW1(Int(Mid(nikMillionV, 2, 1))) +
	'				" " + WholeNumberWMillionSs
	'			Else
	'				zZZMil = WholeNumberW20T90(Int(Mid(nikMillionV, 1, 1))) + " " + WholeNumberWMillionSs
	'			End If
	'		Case 3
	'			zZZMil = WholeNumberW100(Int(Mid(nikMillionV, 1, 1)))
	'			yYMil = Int(Mid(nikMillionV, 2, 1))
	'			XyMil = Int(Mid(nikMillionV, 2, 2))

	'			If yYMil < 2 And Int(Mid(nikMillionV, 3, 1)) <> 0 Then
	'				zZZMil = zZZMil + " и " + WholeNumberW1(XyMil)
	'			ElseIf yYMil >= 2 And Int(Mid(nikMillionV, 3, 1)) <> 0 Then
	'				zZZMil = zZZMil + "," + WholeNumberW20T90(Int(Mid(nikMillionV, 2, 1))) + " и " + WholeNumberW1(Int(Mid(nikMillionV, 3, 1)))
	'			ElseIf yYMil >= 2 And Int(Mid(nikMillionV, 3, 1)) = 0 Then
	'				zZZMil = zZZMil + " и " + WholeNumberW20T90(Int(Mid(nikMillionV, 2, 1)))
	'			End If
	'			zZZMil = zZZMil + " " + WholeNumberWMillionSs
	'	End Select

	'	nikV456 = Mid(Right(nikMillionV, 6), 1, 3)
	'	If Math.Abs(nikV456) <> 0 Then
	'		IaLenTho456 = Int(Len(nikV456))
	'		Select Case IaLenTho456
	'			Case 1
	'				ThousMil = Int(Mid(nikV456, 1, 1))
	'				If Math.Abs(Right(nikMillionV, 3)) = 0 Then
	'					If nikV456 >= 3 Then
	'						zZZMil = zZZMil + " и " + WholeNumberW1(nikV456) + " " + WholeNumberW1000S
	'					ElseIf nikV456 < 3 Then
	'						zZZMil = zZZMil + " и " + WholeNumberW1000(nikV456)
	'					End If
	'				Else
	'					If nikV456 >= 3 Then
	'						zZZMil = zZZMil + "," + WholeNumberW1(nikV456) + " " + WholeNumberW1000S
	'					ElseIf nikV456 < 3 Then
	'						zZZMil = zZZMil + "," + WholeNumberW1000(nikV456)
	'					End If
	'				End If
	'			Case 2
	'				yYMil = Int(Mid(nikV456, 1, 1))
	'				XyMil = Int(Mid(nikV456, 1, 2))
	'				If Abs(Right(nikMillionV, 3)) = 0 Then
	'					If yYMil < 2 Then
	'						zZZMil = zZZMil + " и " + WholeNumberW1(XyMil) + " " + WholeNumberW1000S
	'					ElseIf yYMil >= 2 And Int(Mid(nikV456, 2, 1)) <> 0 Then
	'						zZZMil = zZZMil + " и " + WholeNumberW20T90(Int(Mid(nikV456, 1, 1))) + " и " + WholeNumberW1(Int(Mid(nikV456, 2, 1))) +
	'						" " + WholeNumberW1000S
	'					Else
	'						zZZMil = zZZMil + " и " + WholeNumberW20T90(Int(Mid(nikV456, 1, 1))) + " " + WholeNumberW1000S
	'					End If
	'				Else
	'					If yYMil < 2 Then
	'						zZZMil = zZZMil + "," + WholeNumberW1(XyMil) + " " + WholeNumberW1000S
	'					ElseIf yYMil >= 2 And Int(Mid(nikV456, 2, 1)) <> 0 Then
	'						zZZMil = zZZMil + "," + WholeNumberW20T90(Int(Mid(nikV456, 1, 1))) + " и " + WholeNumberW1(Int(Mid(nikV456, 2, 1))) +
	'						" " + WholeNumberW1000S
	'					Else
	'						zZZMil = zZZMil + "," + WholeNumberW20T90(Int(Mid(nikV456, 1, 1))) + " " + WholeNumberW1000S
	'					End If
	'				End If
	'			Case 3
	'				'            If Abs(Right(nikMillionV, 3)) = 0 Then
	'				'            Else
	'				'            End If
	'				zZZMil = zZZMil + "," + WholeNumberW100(Int(Mid(nikV456, 1, 1)))
	'				yYMil = Int(Mid(nikV456, 2, 1))
	'				XyMil = Int(Mid(nikV456, 2, 2))

	'				If yYMil < 2 And Int(Mid(nikV456, 3, 1)) <> 0 Then
	'					If Int(Mid(nikV456, 3, 1)) = 1 Then
	'						zZZMil = zZZMil + " и една"
	'					ElseIf Int(Mid(nikV456, 3, 1)) = 2 Then
	'						zZZMil = zZZMil + " и две"
	'					Else
	'						zZZMil = zZZMil + " и " + WholeNumberW1(XyMil)
	'					End If
	'				ElseIf yYMil >= 2 And Int(Mid(nikV456, 3, 1)) <> 0 Then
	'					zZZMil = zZZMil + "," + WholeNumberW20T90(Int(Mid(nikV456, 2, 1))) + " и " + WholeNumberW1(Int(Mid(nikV456, 3, 1)))
	'				ElseIf yYMil >= 2 And Int(Mid(nikV456, 3, 1)) = 0 Then
	'					zZZMil = zZZMil + " и " + WholeNumberW20T90(Int(Mid(nikV456, 2, 1)))
	'				End If
	'				zZZMil = zZZMil + " " + WholeNumberW1000S
	'		End Select
	'	End If
	'	If Abs(Right(nikMillionV, 3)) <> 0 Then
	'		nikMil123 = CStr(Abs(Right(nikMillionV, 3)))
	'		IaLenMil123 = Len(nikMil123) 'Len(Abs(Right(nikValueStr, 3)))
	'		Select Case IaLenMil123
	'			Case 1
	'				yYMil = Int(Mid(nikMil123, 1, 1))
	'				If yYMil <> 0 Then zZZMil = zZZMil + " и " + WholeNumberW1(yYMil)
	'			Case 2
	'				yYMil = Int(Mid(nikMil123, 1, 1))
	'				XyMil = Int(Mid(nikMil123, 1, 2))
	'				If yYMil < 2 Then
	'					zZZMil = zZZMil + " и " + WholeNumberW1(XyMil)
	'				ElseIf yYMil >= 2 And Int(Mid(nikMil123, 2, 1)) <> 0 Then
	'					zZZMil = zZZMil + "," + WholeNumberW20T90(Int(Mid(nikMil123, 1, 1))) + " и " + WholeNumberW1(Int(Mid(nikMil123, 2, 1)))
	'				Else
	'					zZZMil = zZZMil + " и " + WholeNumberW20T90(Int(Mid(nikMil123, 1, 1)))
	'				End If
	'			Case 3
	'				If Int(Mid(nikMil123, 2, 1)) = 0 And Int(Mid(nikMil123, 3, 1)) = 0 Then
	'					zZZMil = zZZMil + " и " + WholeNumberW100(Int(Mid(nikMil123, 1, 1)))
	'				Else
	'					zZZMil = zZZMil + "," + WholeNumberW100(Int(Mid(nikMil123, 1, 1)))
	'					yYMil = Int(Mid(nikMil123, 2, 1))
	'					XyMil = Int(Mid(nikMil123, 2, 2))

	'					If yYMil < 2 Then  'And Int(MID(nikV123, 3, 1)) <> 0
	'						zZZMil = zZZMil + " и " + WholeNumberW1(XyMil)
	'					ElseIf yYMil >= 2 And Int(Mid(nikMil123, 3, 1)) <> 0 Then
	'						zZZMil = zZZMil + "," + WholeNumberW20T90(Int(Mid(nikMil123, 2, 1))) + " и " + WholeNumberW1(Int(Mid(nikMil123, 3, 1)))
	'					Else
	'						zZZMil = zZZMil + " и " + WholeNumberW20T90(Int(Mid(nikMil123, 2, 1)))
	'					End If
	'				End If

	'		End Select
	'	End If
	'	MillionInWords = zZZMil

	'End Function

End Class

'Public Const nikWholeNum0 As String = "нула"
'Public Const  As String = "един"
'Public Const nikWholeNum2 As String = "два"
'Public Const nikWholeNum3 As String = "три"
'Public Const nikWholeNum4 As String = "четири"
'Public Const nikWholeNum5 As String = "пет"
'Public Const nikWholeNum6 As String = "шест"
'Public Const nikWholeNum7 As String = "седем"
'Public Const nikWholeNum8 As String = "осем"
'Public Const nikWholeNum9 As String = "девет"
'Public Const nikWholeNumberW11 As String = "единадесет"
'Public Const nikWholeNumberW19 As String = "надесет"

'Public Const nikWholeNumHundred1 As String = "сто"
'Public Const nikWholeNumHundred2 As String = "двеста"
'Public Const nikWholeNumHundred3 As String = "триста"
'Public Const nikWholeNumHundred4 As String = "стотин"

'Public Const nikWholeNumThousand1 As String = "хиляда"
'Public Const nikWholeNumThousand2 As String = "две хиляди"
'Public Const nikWholeNumThousands As String = "хиляди"
'Public Const nikWholeNumMillion As String = "милион"
'Public Const nikWholeNumMillard As String = "милиард"