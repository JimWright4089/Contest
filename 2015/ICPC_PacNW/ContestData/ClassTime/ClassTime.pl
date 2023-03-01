scalar <> ;
while (<>) {
   chomp ;
   push @a, join " ", reverse split " " ;
}
@a = sort @a ;
print((join " ", reverse split " "), "\n") for @a ;
