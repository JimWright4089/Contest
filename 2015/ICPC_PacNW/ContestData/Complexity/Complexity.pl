while (<>) {
   @cnt = (0) x 128 ;
   chomp ;
   $cnt[ord($_)]++ for split '' ;
   @cnt = sort @cnt ;
   print length($_) - $cnt[127] - $cnt[126], "\n" ;
}
