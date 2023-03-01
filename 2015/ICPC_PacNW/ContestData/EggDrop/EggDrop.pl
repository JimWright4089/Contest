$lin = <> ;
chomp $lin ;
($n, $k) = split " ", $lin ;
my $hi = $k - 1 ;
my $lo = 2 ;
while (<>) {
   $f = (split " ")[0] ;
   $hi = $f - 1 if /BROKEN/ && $f <= $hi ;
   $lo = $f + 1 if /SAFE/ && $f >= $lo ;
}
print "$lo $hi\n" ;
