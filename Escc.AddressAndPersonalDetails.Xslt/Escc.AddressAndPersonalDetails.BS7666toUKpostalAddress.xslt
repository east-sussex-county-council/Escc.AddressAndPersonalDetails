<?xml version="1.0" encoding="UTF-8"?>
<!--
Author: Adam Bailin / Bruce Yeoman
Date: 27 October 2004
Description:	This stylesheet can be used to transform BS7666 v2.0 address data into UK Postal Address form.
-->
<xsl:stylesheet version="1.0" 
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform" 
	xmlns:bs7666="http://www.govtalk.gov.uk/people/bs7666" 
	xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
	<xsl:output method="xml" version="1.0" encoding="UTF-8" indent="yes"/>
	<xsl:template match="bs7666:BS7666Address">
		<!-- set up the various parts of the SAON as variables -->
		<xsl:variable name="strSecObjNum">
			<xsl:value-of select="bs7666:SAON/bs7666:StartRange/bs7666:Number"/>
		</xsl:variable>
		<xsl:variable name="strSecObjSuf">
			<xsl:value-of select="bs7666:SAON/bs7666:StartRange/bs7666:Suffix"/>
		</xsl:variable>
		<xsl:variable name="strSecObjRng">
			<xsl:value-of select="bs7666:SAON/bs7666:EndRange/bs7666:Number"/>
		</xsl:variable>
		<xsl:variable name="strSecObjRsf">
			<xsl:value-of select="bs7666:SAON/bs7666:EndRange/bs7666:Suffix"/>
		</xsl:variable>
		<xsl:variable name="SAODescription">
			<xsl:value-of select="bs7666:SAON/bs7666:Description"/>
		</xsl:variable>
		<xsl:variable name="SAOPropertyNumber">
			<xsl:choose>
				<xsl:when test="string-length($strSecObjNum)>0">
					<xsl:value-of select="$strSecObjNum"/>
					<xsl:value-of select="$strSecObjSuf"/>
					<xsl:choose>
						<xsl:when test="string-length($strSecObjRng)>0"> - <xsl:value-of select="$strSecObjRng"/>
							<xsl:value-of select="$strSecObjRsf"/>
						</xsl:when>
					</xsl:choose>
				</xsl:when>
			</xsl:choose>
		</xsl:variable>
		<!-- set up the various parts of the PAON as variables -->
		<xsl:variable name="strPriObjNum">
			<xsl:value-of select="bs7666:PAON/bs7666:StartRange/bs7666:Number"/>
		</xsl:variable>
		<xsl:variable name="strPriObjSuf">
			<xsl:value-of select="bs7666:PAON/bs7666:StartRange/bs7666:Suffix"/>
		</xsl:variable>
		<xsl:variable name="strPriObjRng">
			<xsl:value-of select="bs7666:PAON/bs7666:EndRange/bs7666:Number"/>
		</xsl:variable>
		<xsl:variable name="strPriObjRsf">
			<xsl:value-of select="bs7666:PAON/bs7666:EndRange/bs7666:Suffix"/>
		</xsl:variable>
		<xsl:variable name="PAODescription">
			<xsl:value-of select="bs7666:PAON/bs7666:Description"/>
		</xsl:variable>
		<xsl:variable name="PAOPropertyNumber">
			<xsl:choose>
				<xsl:when test="string-length($strPriObjNum)>0">
					<xsl:value-of select="$strPriObjNum"/>
					<xsl:value-of select="$strPriObjSuf"/>
					<xsl:choose>
						<xsl:when test="string-length($strPriObjRng)>0"> - <xsl:value-of select="$strPriObjRng"/>
							<xsl:value-of select="$strPriObjRsf"/>
						</xsl:when>
					</xsl:choose>
				</xsl:when>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="Street">
			<xsl:value-of select="bs7666:StreetDescription"/>
		</xsl:variable>
		<xsl:variable name="Locality">
			<xsl:value-of select="bs7666:Locality"/>
		</xsl:variable>
		<xsl:variable name="Town">
			<xsl:value-of select="bs7666:Town"/>
		</xsl:variable>
		<xsl:variable name="PostTown">
			<xsl:value-of select="bs7666:PostTown"/>
		</xsl:variable>
		<xsl:variable name="AdministrativeArea">
			<xsl:value-of select="bs7666:AdministrativeArea"/>
		</xsl:variable>
		<xsl:variable name="PostCode">
			<xsl:value-of select="bs7666:PostCode"/>
		</xsl:variable>
		<!-- now create a set of booleans which identify which bits we have -->
		<xsl:variable name="bSAODescription">
			<xsl:choose>
				<xsl:when test="string-length($SAODescription)=0">N</xsl:when>
				<xsl:otherwise>Y</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="bSAOPropertyNumber">
			<xsl:choose>
				<xsl:when test="string-length($SAOPropertyNumber)=0">N</xsl:when>
				<xsl:otherwise>Y</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="bPAODescription">
			<xsl:choose>
				<xsl:when test="string-length($PAODescription)=0">N</xsl:when>
				<xsl:otherwise>Y</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="bPAOPropertyNumber">
			<xsl:choose>
				<xsl:when test="string-length($PAOPropertyNumber)=0">N</xsl:when>
				<xsl:otherwise>Y</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="bStreet">
			<xsl:choose>
				<xsl:when test="string-length($Street)=0">N</xsl:when>
				<xsl:otherwise>Y</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="bLocality">
			<xsl:choose>
				<xsl:when test="string-length($Locality)=0">N</xsl:when>
				<xsl:otherwise>Y</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="bTown">
			<xsl:choose>
				<xsl:when test="string-length($Town)=0">N</xsl:when>
				<xsl:when test="$Town=$PostTown">N</xsl:when>
				<xsl:otherwise>Y</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="bPostTown">
			<xsl:choose>
				<xsl:when test="string-length($PostTown)=0">N</xsl:when>
				<xsl:otherwise>Y</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="bAdministrativeArea">
			<xsl:choose>
				<xsl:when test="string-length($AdministrativeArea)=0">N</xsl:when>
				<xsl:otherwise>Y</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="bPostCode">
			<xsl:choose>
				<xsl:when test="string-length($PostCode)=0">N</xsl:when>
				<xsl:otherwise>Y</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="PCaddressLine">
			<xsl:choose>
				<xsl:when test="$bPostCode='Y'">
					<xsl:value-of select="$PostCode"/>
				</xsl:when>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="AddressLine0">
			<xsl:choose>
				<xsl:when test="$bSAODescription='Y'">
					<xsl:value-of select="$SAODescription"/>
				</xsl:when>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="LineCount0">
			<xsl:choose>
				<xsl:when test="$bSAODescription='N'">0</xsl:when>
				<xsl:otherwise>
					<xsl:number value="1"/>
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="AddressLine1">
			<!-- pSAODescription on line 1 and there is either SAOPropertyNumber or PAODescription -->
			<!-- so output them-->
			<xsl:choose>
				<xsl:when test="($bSAOPropertyNumber='Y') and ($bPAODescription='N')">
					<xsl:value-of select="$SAOPropertyNumber"/>
				</xsl:when>
				<xsl:when test="($bSAOPropertyNumber='Y') and ($bPAODescription='Y')">
					<xsl:value-of select="$SAOPropertyNumber"/>, <xsl:value-of select="$PAODescription"/>
				</xsl:when>
				<xsl:when test="($bSAOPropertyNumber='N') and ($bPAODescription='Y')">
					<xsl:value-of select="$PAODescription"/>
				</xsl:when>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="LineCount1">
			<xsl:choose>
				<xsl:when test="string-length($AddressLine1)=0">0</xsl:when>
				<xsl:otherwise>1</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="AddressLine2">
			<xsl:choose>
				<xsl:when test="$bPAOPropertyNumber='Y'">
					<xsl:value-of select="$PAOPropertyNumber"/>, <xsl:value-of select="$Street"/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$Street"/>
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="LineCount2">
			<xsl:choose>
				<xsl:when test="string-length($AddressLine2)=0">0</xsl:when>
				<xsl:otherwise>1</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="LineCount3">
			<xsl:choose>
				<xsl:when test="$bLocality='N'">0</xsl:when>
				<xsl:otherwise>1</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="LineCount4">
			<xsl:choose>
				<xsl:when test="$bTown='N'">0</xsl:when>
				<xsl:otherwise>1</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="LineCount5">
			<xsl:choose>
				<xsl:when test="($bAdministrativeArea='N') and ($bPostTown='N')">0</xsl:when>
				<xsl:otherwise>1</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="LineCount">
			<xsl:number value="number($LineCount0) + number($LineCount1) + number($LineCount2) + number($LineCount3) + number($LineCount4) + number($LineCount5)"/>
		</xsl:variable>
		<xsl:variable name="AddressLine3">
			<!-- PAOPropertyNumber and Street have already been output in AddressLine 2 -->
			<!-- if there are more than 5 lines output loaclity & Town otherwise just Locality -->
			<xsl:choose>
				<xsl:when test="($bLocality='Y') and ($bTown='Y') and ($LineCount&gt;5)">
					<xsl:value-of select="$Locality"/>, <xsl:value-of select="$Town"/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:choose>
						<xsl:when test="$bLocality='Y'">
							<xsl:value-of select="$Locality"/>
						</xsl:when>
					</xsl:choose>
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="AddressLine4">
			<xsl:choose>
				<xsl:when test="($bTown='Y') and ($LineCount&lt;6)">
					<xsl:value-of select="$Town"/>
				</xsl:when>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="AddressLine5">
			<xsl:choose>
				<xsl:when test="($bAdministrativeArea='N') or (($bPostCode='Y') and ($bPostTown='Y'))">
					<xsl:value-of select="$PostTown"/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$AdministrativeArea"/>
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>

<UKPostalAddress xmlns="http://www.govtalk.gov.uk/people/AddressAndPersonalDetails" 
	xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" 
	xsi:schemaLocation="http://www.govtalk.gov.uk/people/AddressAndPersonalDetails AddressTypes-v1-3.xsd">
		<xsl:choose>
			<xsl:when test="string-length($AddressLine0)>0">
					<Line><xsl:value-of select="$AddressLine0"/></Line>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
		<xsl:choose>
			<xsl:when test="string-length($AddressLine1)>0">
					<Line><xsl:value-of select="$AddressLine1"/></Line>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
		<xsl:choose>
			<xsl:when test="string-length($AddressLine2)>0">
					<Line><xsl:value-of select="$AddressLine2"/></Line>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
		<xsl:choose>
			<xsl:when test="string-length($AddressLine3)>0">
					<Line><xsl:value-of select="$AddressLine3"/></Line>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
		<xsl:choose>
			<xsl:when test="string-length($AddressLine4)>0">
					<Line><xsl:value-of select="$AddressLine4"/></Line>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
		<xsl:choose>
			<xsl:when test="string-length($AddressLine5)>0">
					<Line><xsl:value-of select="$AddressLine5"/></Line>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
		<xsl:choose>
			<xsl:when test="string-length($PCaddressLine)>0">
					<PostCode><xsl:value-of select="$PCaddressLine"/></PostCode>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
</UKPostalAddress>

	</xsl:template>
	
</xsl:stylesheet>
