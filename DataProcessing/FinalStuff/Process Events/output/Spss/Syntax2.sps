﻿* Encoding: UTF-8.
* Encoding: .
FREQUENCIES VARIABLES=Sex Age Occupation How.often.do.you.play.video.games
    Do.you.enjoy.games.that.offer.companions.to.aid.your.journeys
  /STATISTICS=STDDEV MINIMUM MAXIMUM MEAN
  /ORDER=ANALYSIS.

*Test if parametric.
EXAMINE VARIABLES=CompSC_EXPERIENCE_RATE CompSF_EXPERIENCE_RATE CompOF_EXPERIENCE_RATE
    CompOC_EXPERIENCE_RATE BY X.I.see.myself.as.a
  /PLOT BOXPLOT STEMLEAF NPPLOT
  /COMPARE GROUPS
  /STATISTICS DESCRIPTIVES
  /CINTERVAL 95
  /MISSING LISTWISE
  /NOTOTAL.

*Test if parametric.
EXAMINE VARIABLES=CompSC_EXPERIENCE_RATE CompSF_EXPERIENCE_RATE CompOF_EXPERIENCE_RATE
    CompOC_EXPERIENCE_RATE BY OBSERVED_QUADRANT
  /PLOT BOXPLOT STEMLEAF NPPLOT
  /COMPARE GROUPS
  /STATISTICS DESCRIPTIVES
  /CINTERVAL 95
  /MISSING LISTWISE
  /NOTOTAL.

*Test if parametric.
EXAMINE VARIABLES=GEQ_SCORE_SC_PSYCHOLOGICAL_INVOLVEMENT_EMPATHY
    GEQ_SCORE_SF_PSYCHOLOGICAL_INVOLVEMENT_EMPATHY GEQ_SCORE_OC_PSYCHOLOGICAL_INVOLVEMENT_EMPATHY
    GEQ_SCORE_OF_PSYCHOLOGICAL_INVOLVEMENT_EMPATHY BY X.I.see.myself.as.a
  /PLOT BOXPLOT STEMLEAF NPPLOT
  /COMPARE GROUPS
  /STATISTICS DESCRIPTIVES
  /CINTERVAL 95
  /MISSING LISTWISE
  /NOTOTAL.

*Test if parametric.
EXAMINE VARIABLES=GEQ_SCORE_SC_PSYCHOLOGICAL_INVOLVEMENT_EMPATHY
    GEQ_SCORE_SF_PSYCHOLOGICAL_INVOLVEMENT_EMPATHY GEQ_SCORE_OC_PSYCHOLOGICAL_INVOLVEMENT_EMPATHY
    GEQ_SCORE_OF_PSYCHOLOGICAL_INVOLVEMENT_EMPATHY BY OBSERVED_QUADRANT
  /PLOT BOXPLOT STEMLEAF NPPLOT
  /COMPARE GROUPS
  /STATISTICS DESCRIPTIVES
  /CINTERVAL 95
  /MISSING LISTWISE
  /NOTOTAL.

*Not parametric split file for pref_player.
SORT CASES  BY X.I.see.myself.as.a.
SPLIT FILE SEPARATE BY X.I.see.myself.as.a.

*Friedman Pref_Player with EXP_RATE.
NPAR TESTS
  /FRIEDMAN=CompSC_EXPERIENCE_RATE CompSF_EXPERIENCE_RATE CompOC_EXPERIENCE_RATE
    CompOF_EXPERIENCE_RATE
  /MISSING LISTWISE
  /METHOD=EXACT TIMER(5).

*Split for Pref_Player_Observed with EXP_RATE.
SORT CASES  BY OBSERVED_QUADRANT.
SPLIT FILE SEPARATE BY OBSERVED_QUADRANT.

*Friedman Pref_Player_Observed with EXP_RATE.
NPAR TESTS
  /FRIEDMAN=CompSC_EXPERIENCE_RATE CompSF_EXPERIENCE_RATE CompOC_EXPERIENCE_RATE
    CompOF_EXPERIENCE_RATE
  /MISSING LISTWISE
  /METHOD=EXACT TIMER(5).

SORT CASES  BY X.I.see.myself.as.a.
SPLIT FILE SEPARATE BY X.I.see.myself.as.a.

*Friedman Pref_Player with GEQ_EMPATHY..
NPAR TESTS
  /FRIEDMAN=GEQ_SCORE_SC_PSYCHOLOGICAL_INVOLVEMENT_EMPATHY
    GEQ_SCORE_SF_PSYCHOLOGICAL_INVOLVEMENT_EMPATHY GEQ_SCORE_OC_PSYCHOLOGICAL_INVOLVEMENT_EMPATHY
    GEQ_SCORE_OF_PSYCHOLOGICAL_INVOLVEMENT_EMPATHY
  /MISSING LISTWISE
  /METHOD=EXACT TIMER(5).

*Friedman Pref_Player with GEQ_NEGATIVE_FEELINGS.
NPAR TESTS
  /FRIEDMAN=GEQ_SCORE_SC_PSYCHOLOGICAL_INVOLVEMENT_NEGATIVE_FEELINGS
    GEQ_SCORE_SF_PSYCHOLOGICAL_INVOLVEMENT_NEGATIVE_FEELINGS
    GEQ_SCORE_OC_PSYCHOLOGICAL_INVOLVEMENT_NEGATIVE_FEELINGS
    GEQ_SCORE_OF_PSYCHOLOGICAL_INVOLVEMENT_NEGATIVE_FEELINGS
  /MISSING LISTWISE
  /METHOD=EXACT TIMER(5).

*Friedman Pref_Player with GEQ_BEHAVIORAL_INVOLVMENT.
NPAR TESTS
  /FRIEDMAN=GEQ_SCORE_SC_PSYCHOLOGICAL_BEHAVIOURAL_INVOLVEMENT
    GEQ_SCORE_SF_PSYCHOLOGICAL_BEHAVIOURAL_INVOLVEMENT
    GEQ_SCORE_OC_PSYCHOLOGICAL_BEHAVIOURAL_INVOLVEMENT
    GEQ_SCORE_OF_PSYCHOLOGICAL_BEHAVIOURAL_INVOLVEMENT
  /MISSING LISTWISE
  /METHOD=EXACT TIMER(5).

*Split for Pref_Player_Observed with EXP_RATE.
SORT CASES  BY OBSERVED_QUADRANT.
SPLIT FILE SEPARATE BY OBSERVED_QUADRANT.

*Friedman Pref_Player with GEQ_EMPATHY..
NPAR TESTS
  /FRIEDMAN=GEQ_SCORE_SC_PSYCHOLOGICAL_INVOLVEMENT_EMPATHY
    GEQ_SCORE_SF_PSYCHOLOGICAL_INVOLVEMENT_EMPATHY GEQ_SCORE_OC_PSYCHOLOGICAL_INVOLVEMENT_EMPATHY
    GEQ_SCORE_OF_PSYCHOLOGICAL_INVOLVEMENT_EMPATHY
  /MISSING LISTWISE
  /METHOD=EXACT TIMER(5).

*Friedman Pref_Player with GEQ_NEGATIVE_FEELINGS.
NPAR TESTS
  /FRIEDMAN=GEQ_SCORE_SC_PSYCHOLOGICAL_INVOLVEMENT_NEGATIVE_FEELINGS
    GEQ_SCORE_SF_PSYCHOLOGICAL_INVOLVEMENT_NEGATIVE_FEELINGS
    GEQ_SCORE_OC_PSYCHOLOGICAL_INVOLVEMENT_NEGATIVE_FEELINGS
    GEQ_SCORE_OF_PSYCHOLOGICAL_INVOLVEMENT_NEGATIVE_FEELINGS
  /MISSING LISTWISE
  /METHOD=EXACT TIMER(5).

*Friedman Pref_Player with GEQ_BEHAVIORAL_INVOLVMENT.
NPAR TESTS
  /FRIEDMAN=GEQ_SCORE_SC_PSYCHOLOGICAL_BEHAVIOURAL_INVOLVEMENT
    GEQ_SCORE_SF_PSYCHOLOGICAL_BEHAVIOURAL_INVOLVEMENT
    GEQ_SCORE_OC_PSYCHOLOGICAL_BEHAVIOURAL_INVOLVEMENT
    GEQ_SCORE_OF_PSYCHOLOGICAL_BEHAVIOURAL_INVOLVEMENT
  /MISSING LISTWISE
  /METHOD=EXACT TIMER(5).

*Check if score differences are significative.
SPLIT FILE OFF. 
NPAR TESTS 
  /FRIEDMAN=CompOC_FINAL CompOF_FINAL CompSC_FINAL CompSF_FINAL 
  /MISSING LISTWISE 
  /METHOD=EXACT TIMER(5).

*See if IMI is normal -> It is, then kruskal-wallis. Mas vamos fazer uma ANOVA univariante.
EXAMINE VARIABLES=IMI_INTEREST_ENJOYMENT_SCORE BY X.I.see.myself.as.a
  /PLOT BOXPLOT STEMLEAF NPPLOT
  /COMPARE GROUPS
  /STATISTICS DESCRIPTIVES
  /CINTERVAL 95
  /MISSING LISTWISE
  /NOTOTAL.

*ANOVA UNI.
UNIANOVA IMI_INTEREST_ENJOYMENT_SCORE BY X.I.see.myself.as.a
  /METHOD=SSTYPE(3)
  /INTERCEPT=INCLUDE
  /CRITERIA=ALPHA(0.05)
  /DESIGN=X.I.see.myself.as.a.

*ONE-WAY ANOVA.
ONEWAY IMI_INTEREST_ENJOYMENT_SCORE BY X.I.see.myself.as.a
  /MISSING ANALYSIS
  /CRITERIA=CILEVEL(0.95).

*Kruskal-walis nao significativo.
NPAR TESTS
  /K-W=IMI_INTEREST_ENJOYMENT_SCORE BY X.I.see.myself.as.a(0 4)
  /MISSING ANALYSIS.

*See if IMI is normal -> It is, then kruskal-wallis. Mas vamos fazer uma ANOVA univariante.
EXAMINE VARIABLES=IMI_INTEREST_ENJOYMENT_SCORE BY OBSERVED_QUADRANT
  /PLOT BOXPLOT STEMLEAF NPPLOT
  /COMPARE GROUPS
  /STATISTICS DESCRIPTIVES
  /CINTERVAL 95
  /MISSING LISTWISE
  /NOTOTAL.

*ANOVA UNI FOR OBSERVED.
UNIANOVA IMI_INTEREST_ENJOYMENT_SCORE BY OBSERVED_QUADRANT
  /METHOD=SSTYPE(3)
  /INTERCEPT=INCLUDE
  /CRITERIA=ALPHA(0.05)
  /DESIGN=OBSERVED_QUADRANT.

*ONE-WAY ANOVA FOR OBSERVED.
ONEWAY IMI_INTEREST_ENJOYMENT_SCORE BY OBSERVED_QUADRANT
  /MISSING ANALYSIS
  /CRITERIA=CILEVEL(0.95).

*Kruskal-walis nao significativo FOR OBSERVED.
NPAR TESTS
  /K-W=IMI_INTEREST_ENJOYMENT_SCORE BY OBSERVED_QUADRANT(0 4)
  /MISSING ANALYSIS.

*Correlation Pearson Companion with Enjoyment.
CORRELATIONS
  /VARIABLES=CompSC_EXPERIENCE_RATE CompSF_EXPERIENCE_RATE CompOC_EXPERIENCE_RATE
    CompOF_EXPERIENCE_RATE GEQ_SCORE_SC_PSYCHOLOGICAL_INVOLVEMENT_EMPATHY
    GEQ_SCORE_SC_PSYCHOLOGICAL_INVOLVEMENT_NEGATIVE_FEELINGS
    GEQ_SCORE_SC_PSYCHOLOGICAL_BEHAVIOURAL_INVOLVEMENT GEQ_SCORE_SF_PSYCHOLOGICAL_INVOLVEMENT_EMPATHY
    GEQ_SCORE_SF_PSYCHOLOGICAL_INVOLVEMENT_NEGATIVE_FEELINGS
    GEQ_SCORE_SF_PSYCHOLOGICAL_BEHAVIOURAL_INVOLVEMENT GEQ_SCORE_OC_PSYCHOLOGICAL_INVOLVEMENT_EMPATHY
    GEQ_SCORE_OC_PSYCHOLOGICAL_INVOLVEMENT_NEGATIVE_FEELINGS
    GEQ_SCORE_OC_PSYCHOLOGICAL_BEHAVIOURAL_INVOLVEMENT GEQ_SCORE_OF_PSYCHOLOGICAL_INVOLVEMENT_EMPATHY
    GEQ_SCORE_OF_PSYCHOLOGICAL_INVOLVEMENT_NEGATIVE_FEELINGS
    GEQ_SCORE_OF_PSYCHOLOGICAL_BEHAVIOURAL_INVOLVEMENT
  /PRINT=TWOTAIL NOSIG FULL
  /MISSING=PAIRWISE.

*Correlation Spearman Companion with Enjoyment.
NONPAR CORR
  /VARIABLES=CompSC_EXPERIENCE_RATE CompSF_EXPERIENCE_RATE CompOC_EXPERIENCE_RATE
    CompOF_EXPERIENCE_RATE GEQ_SCORE_SC_PSYCHOLOGICAL_INVOLVEMENT_EMPATHY
    GEQ_SCORE_SC_PSYCHOLOGICAL_INVOLVEMENT_NEGATIVE_FEELINGS
    GEQ_SCORE_SC_PSYCHOLOGICAL_BEHAVIOURAL_INVOLVEMENT GEQ_SCORE_SF_PSYCHOLOGICAL_INVOLVEMENT_EMPATHY
    GEQ_SCORE_SF_PSYCHOLOGICAL_INVOLVEMENT_NEGATIVE_FEELINGS
    GEQ_SCORE_SF_PSYCHOLOGICAL_BEHAVIOURAL_INVOLVEMENT GEQ_SCORE_OC_PSYCHOLOGICAL_INVOLVEMENT_EMPATHY
    GEQ_SCORE_OC_PSYCHOLOGICAL_INVOLVEMENT_NEGATIVE_FEELINGS
    GEQ_SCORE_OC_PSYCHOLOGICAL_BEHAVIOURAL_INVOLVEMENT GEQ_SCORE_OF_PSYCHOLOGICAL_INVOLVEMENT_EMPATHY
    GEQ_SCORE_OF_PSYCHOLOGICAL_INVOLVEMENT_NEGATIVE_FEELINGS
    GEQ_SCORE_OF_PSYCHOLOGICAL_BEHAVIOURAL_INVOLVEMENT
  /PRINT=SPEARMAN TWOTAIL NOSIG FULL
  /MISSING=PAIRWISE.

OUTPUT MODIFY
  /REPORT PRINTREPORT=YES
  /SELECT  TABLES
  /DELETEOBJECT DELETE=NO
  /OBJECTPROPERTIES   VISIBLE=ASIS
  /TABLECELLS SELECT=[BODY] SELECTCONDITION="Abs(x)<=0.05" STYLE=REGULAR BOLD
    BACKGROUNDCOLOR=RGB(251, 248, 115) APPLYTO=CELL
  /TABLECELLS SELECT=[CORRELATION] SELECTDIMENSION=COLUMNS SELECTCONDITION=ALL REVERTTODEFAULT=YES
    APPLYTO=CELL.

*Not parametric split file for pref_player.
SORT CASES  BY X.I.see.myself.as.a.
SPLIT FILE SEPARATE BY X.I.see.myself.as.a.

*Pairwise Comparisons.
*Nonparametric Tests: Related Samples. 
NPTESTS 
  /RELATED TEST(CompSC_EXPERIENCE_RATE CompSF_EXPERIENCE_RATE CompOC_EXPERIENCE_RATE CompOF_EXPERIENCE_RATE) FRIEDMAN(COMPARE=PAIRWISE) 
  /MISSING SCOPE=ANALYSIS USERMISSING=EXCLUDE 
  /CRITERIA ALPHA=0.06  CILEVEL=95.

*Nonparametric Tests: Related Samples. 
NPTESTS 
  /RELATED TEST(GEQ_SCORE_SC_PSYCHOLOGICAL_INVOLVEMENT_EMPATHY GEQ_SCORE_SF_PSYCHOLOGICAL_INVOLVEMENT_EMPATHY GEQ_SCORE_OC_PSYCHOLOGICAL_INVOLVEMENT_EMPATHY GEQ_SCORE_OF_PSYCHOLOGICAL_INVOLVEMENT_EMPATHY) FRIEDMAN(COMPARE=PAIRWISE) 
  /MISSING SCOPE=ANALYSIS USERMISSING=EXCLUDE 
  /CRITERIA ALPHA=0.05  CILEVEL=95.

*Split for Pref_Player_Observed with EXP_RATE.
SORT CASES  BY OBSERVED_QUADRANT.
SPLIT FILE SEPARATE BY OBSERVED_QUADRANT.

*Pairwise Comparisons.
*Nonparametric Tests: Related Samples. 
NPTESTS 
  /RELATED TEST(CompSC_EXPERIENCE_RATE CompSF_EXPERIENCE_RATE CompOC_EXPERIENCE_RATE CompOF_EXPERIENCE_RATE) FRIEDMAN(COMPARE=PAIRWISE) 
  /MISSING SCOPE=ANALYSIS USERMISSING=EXCLUDE 
  /CRITERIA ALPHA=0.05  CILEVEL=95.

*Nonparametric Tests: Related Samples. 
NPTESTS 
  /RELATED TEST(GEQ_SCORE_SC_PSYCHOLOGICAL_INVOLVEMENT_EMPATHY GEQ_SCORE_SF_PSYCHOLOGICAL_INVOLVEMENT_EMPATHY GEQ_SCORE_OC_PSYCHOLOGICAL_INVOLVEMENT_EMPATHY GEQ_SCORE_OF_PSYCHOLOGICAL_INVOLVEMENT_EMPATHY) FRIEDMAN(COMPARE=PAIRWISE) 
  /MISSING SCOPE=ANALYSIS USERMISSING=EXCLUDE 
  /CRITERIA ALPHA=0.05  CILEVEL=95.
