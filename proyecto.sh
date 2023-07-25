!#/bin/bash
read -p "Choose a number:
          1 run 
          2 slides 
          3 report 
          4 show_report 
          5 show_slides 
          6 clean
" option 

case $option in
    1)
        dotnet watch run --project MoogleServer 
        ;;
    2)
	    pdflatex Presentation.tex
        ;;
    3)
	    pdflatex Informe.tex 
        ;;
    4)
        pdflatex Informe.tex 
	    xdg-open Informe.pdf
        ;;
    5)
        pdflatex Presentation.tex
	    xdg-open Presentation.pdf
        ;;
    6)
	    rm *.log *.fls *.aux *.gz *.nav *.out *.snm *.toc *.fdb_latexmk *.gz *.syntec *.pdf
	    ;;
    *)
        echo "no existe"
        ;;
esac